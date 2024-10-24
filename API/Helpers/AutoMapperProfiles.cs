using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<AppUser, MemberUpdateDto>();
            CreateMap<AppUser, MemberCreateDto>();
            CreateMap<MemberCreateDto, AppUser>();
            CreateMap<MemberDto, AppUser>();
            CreateMap<AppUser, MemberDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>();

            CreateMap<Photo, PhotoDto>();

            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<BlogPostDto, BlogPost>();
            CreateMap<BlogPost, BlogPostUpdateDto>();
            CreateMap<BlogPostUpdateDto, BlogPost>();

            CreateMap<WorkOrder, WorkOrderDto>();
            CreateMap<WorkOrderDto, WorkOrder>();
            CreateMap<WorkOrderStatus, WorkOrderStatusDto>();
            CreateMap<WorkOrderStatusDto, WorkOrderStatus>();

            CreateMap<Issue, IssueDto>();
            CreateMap<IssueDto, Issue>();            
            CreateMap<IssueUpdateDto, Issue>();
            CreateMap<Issue, IssueUpdateDto>();

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();            
            CreateMap<InvoiceItem, InvoiceItemDto>();
            CreateMap<InvoiceItemDto, InvoiceItem>();

            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();

            CreateMap<Quote, QuoteDto>();
            CreateMap<QuoteDto, Quote>();            
            CreateMap<QuoteItem, QuoteItemDto>();
            CreateMap<QuoteItemDto, QuoteItem>();

            CreateMap<Message, MessageDto>();
        }
    }
}