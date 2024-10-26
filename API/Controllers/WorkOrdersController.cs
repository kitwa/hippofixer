using API.Data;
using API.DTOs;
using API.Extensions;
using API.Features.Admin.Queries;
using API.Features.BlogPosts.Commands;
using API.Features.BlogPosts.Queries;
using API.Features.Issues.Commands;
using API.Features.Issues.Queries;
using API.Features.WorkOrders.Queries;
using API.Helpers;
using API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class WorkOrdersController : BaseApiController
    {
        public readonly IMediator _mediator;
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;
        public WorkOrdersController(DataContext context, IPhotoService photoService, IMediator mediator)
        {
            _photoService = photoService;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderDto>>> GetWorkOrders([FromQuery]UserParams userParams)
        {
            var workOrders = await _mediator.Send(new GetWorkOrders.Query(userParams));
            
            Response.AddPaginationHeader(workOrders.CurrentPage, workOrders.PageSize, workOrders.TotalCount, workOrders.TotalPages);    
            return Ok(workOrders);
        }


        [HttpGet("{id}", Name = "GetWorkOrder")]
        public async Task<ActionResult<WorkOrderDto>> GetWorkOrder(int id)
        {
            var workOrder = await _mediator.Send(new GetWorkOrder.Query(id));
            if(workOrder != null) {
                return workOrder;
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/reject")]
        public async Task<ActionResult> RejectWorkOrder(int id)
        {
            var res = await _mediator.Send(new RejectWorkOrder.Command(id));

            if(res != null){
                 return NoContent();
            }
            return BadRequest("Failed to reject workOrder");
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/complete")]
        public async Task<ActionResult> CompleteWorkOrder(int id)
        {
            var res = await _mediator.Send(new CompleteWorkOrder.Command(id));

            if(res != null){
                 return NoContent();
            }
            return BadRequest("Failed to complete workOrder");
        }

    }

}