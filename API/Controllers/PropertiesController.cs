using API.Data;
using API.DTOs;
using API.Extensions;
using API.Features.BlogPosts.Commands;
using API.Features.BlogPosts.Queries;
using API.Helpers;
using API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PropertiesController : BaseApiController
    {
        public readonly IMediator _mediator;
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;
        public PropertiesController(DataContext context, IPhotoService photoService, IMediator mediator)
        {
            _photoService = photoService;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetCars([FromQuery]UserParams userParams)
        {
            var properties = await _mediator.Send(new GetProperties.Query(userParams));
            
            Response.AddPaginationHeader(properties.CurrentPage, properties.PageSize, properties.TotalCount, properties.TotalPages);    
            return Ok(properties);
        }


        [HttpGet("{id}", Name = "GetCar")]
        public async Task<ActionResult<PropertyDto>> GetCar(int id)
        {
            var property = await _mediator.Send(new GetProperty.Query(id));
            if(property != null) {
                return property;
            }
            return NotFound();
        }

        [HttpPost("search-properties")]
        public async Task<ActionResult<PropertyDto>> Search(SearchPropertyDto searchPropertyDto, [FromQuery]UserParams userParams)
        {

            var properties = await _mediator.Send(new SearchProperty.Command(searchPropertyDto, userParams));
            
            Response.AddPaginationHeader(properties.CurrentPage, properties.PageSize, properties.TotalCount, properties.TotalPages);    
            return Ok(properties);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProperty(int id, PropertyUpdateDto carUpdateDto)
        {
            var property = await _mediator.Send(new UpdateProperty.Command(id, carUpdateDto));

            if(property != null){
                return Ok(property);
            }
            return BadRequest("Failed to create property");
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost]
        public async Task<ActionResult<PropertyDto>> CreateProperty(PropertyDto propertyDto)
        {
            var property = await _mediator.Send(new CreateProperty.Command(propertyDto));

            if(property != null){
                return Ok(property);
            }
            return BadRequest("Failed to create blogPost");

        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/delete")]
        public async Task<ActionResult> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}