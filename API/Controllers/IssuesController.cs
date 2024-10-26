using API.Data;
using API.DTOs;
using API.Extensions;
using API.Features.Admin.Queries;
using API.Features.BlogPosts.Commands;
using API.Features.BlogPosts.Queries;
using API.Features.Issues.Commands;
using API.Features.Issues.Queries;
using API.Helpers;
using API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class IssuesController : BaseApiController
    {
        public readonly IMediator _mediator;
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;
        public IssuesController(DataContext context, IPhotoService photoService, IMediator mediator)
        {
            _photoService = photoService;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetCars([FromQuery]UserParams userParams)
        {
            var issues = await _mediator.Send(new GetIssues.Query(userParams));
            
            Response.AddPaginationHeader(issues.CurrentPage, issues.PageSize, issues.TotalCount, issues.TotalPages);    
            return Ok(issues);
        }


        [HttpGet("{id}", Name = "GetIssue")]
        public async Task<ActionResult<IssueDto>> GetCar(int id)
        {
            var issue = await _mediator.Send(new GetIssue.Query(id));
            if(issue != null) {
                return issue;
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIssue(int id, PropertyUpdateDto carUpdateDto)
        {
            var issue = await _mediator.Send(new UpdateIssue.Command(id, carUpdateDto));

            if(issue != null){
                return Ok(issue);
            }
            return BadRequest("Failed to create issue");
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost]
        public async Task<ActionResult<IssueDto>> CreateIssue(IssueDto issueDto)
        {
            var issue = await _mediator.Send(new CreateIssue.Command(issueDto));

            if(issue != null){
                return Ok(issue);
            }
            return BadRequest("Failed to create result");

        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/delete")]
        public async Task<ActionResult> DeleteIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("issue-types")]
        public async Task<ActionResult> GetIssueTypes()
        {
            var result = await _mediator.Send(new GetIssueTypes.Query());
    
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost("{id:int}/add-photo")]
        public async Task<ActionResult<IssueDto>> UploadPhotoForIssue(int id, IFormFile file)
        {
            var result = await _mediator.Send(new UploadPhotoForIssue.Command(id, file));

            if(result != null){
                return Ok(result);
            }
            return BadRequest("Failed uplaod photo");

        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/accept")]
        public async Task<ActionResult> AcceptIssue(int id)
        {
            var res = await _mediator.Send(new AcceptIssue.Command(id, User.GetEmail()));

            if(res != null){
                 return NoContent();
            }
            return BadRequest("Failed to accept issue");
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetCities()
        {
            var cities = await _mediator.Send(new GetCities.Query());
    
            return Ok(cities);
        }

    }

}