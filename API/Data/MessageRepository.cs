using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages
            .Include(u => u.Sender)
            .Include(u => u.Recipient)
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                    .OrderByDescending(messageParams => messageParams.MessageSent)
                    .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.Email == messageParams.Email
                    && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.Email == messageParams.Email),
                _ => query.Where(u => u.Recipient.Email == messageParams.Email && u.RecipientDeleted == false
                    && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);

        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentEmail, string recipientEmail)
        {
            var messages = await _context.Messages
                .Where(m => m.Recipient.Email == currentEmail && m.RecipientDeleted == false
                        && m.Sender.Email == recipientEmail
                        || m.Recipient.Email == recipientEmail
                        && m.Sender.Email == currentEmail && m.SenderDeleted == false
                )
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null 
                && m.RecipientEmail == currentEmail).ToList();

            // if (unreadMessages.Any())
            // {
            //     foreach (var message in unreadMessages)
            //     {
            //         var now = DateTime.Now;
            //         var date = new DateTime(now.Year, now.Month, now.Day,
            //             now.Hour, now.Minute, now.Second);
            //         message.DateRead = message.MessageSent;
            //     }
            //                     await _context.SaveChangesAsync();
            // }

            return messages;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}