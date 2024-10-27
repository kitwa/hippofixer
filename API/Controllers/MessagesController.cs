using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Features.Messages.Commands;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetEmail();
            if (username == createMessageDto.RecipientEmail.ToLower())
                return BadRequest("You cannot send messages to yourself");

            var sender = await _userRepository.GetUserByEmailAsync(User.GetEmail());
            var recipient = await _userRepository.GetUserByEmailAsync(createMessageDto.RecipientEmail);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderEmail = sender.Email,
                RecipientEmail = recipient.Email,
                Content = createMessageDto.Content
            };

            var sendEmail = new SendMessageEmail();


            _messageRepository.AddMessage(message);

            await sendEmail.SendMessageEmailAsync(sender.Email, recipient.Email, recipient.UserName); 

            if(await _messageRepository.SaveAllAsync()) 
            
                return Ok(_mapper.Map<MessageDto>(message));
            else
                return BadRequest("Failed to send message.");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            messageParams.Email = User.GetEmail();

            var messages = await _messageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("thread/{email}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string email)
        {
            var currentEmail = User.GetEmail();
            return Ok(await _messageRepository.GetMessageThread(currentEmail, email));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var email = User.GetEmail();

            var message = await _messageRepository.GetMessage(id);

            if (message.Sender.Email != email && message.Recipient.Email != email)
                return Unauthorized();

            if (message.Sender.Email == email) message.SenderDeleted = true;

            if (message.Recipient.Email == email) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                _messageRepository.DeleteMessage(message);

            // if (await _unitOfWork.Complete()) return Ok();

            if(await _messageRepository.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting the message");
        }
    }
}