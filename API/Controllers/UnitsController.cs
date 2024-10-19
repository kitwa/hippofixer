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

    public class UnitsController : BaseApiController
    {
        public readonly IMediator _mediator;
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;
        public UnitsController(DataContext context, IPhotoService photoService, IMediator mediator)
        {
            _photoService = photoService;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetUnits([FromQuery]UserParams userParams)
        {
            var units = await _mediator.Send(new GetUnits.Query(userParams));
            
            Response.AddPaginationHeader(units.CurrentPage, units.PageSize, units.TotalCount, units.TotalPages);    
            return Ok(units);
        }


        [HttpGet("{id}", Name = "GetUnit")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var car = await _mediator.Send(new GetUnit.Query(id));
            if(car != null) {
                return car;
            }
            return NotFound();
        }

        [HttpPost("search-units")]
        public async Task<ActionResult<UnitDto>> SearchUnit(SearchUnitDto searchUnitDto, [FromQuery]UserParams userParams)
        {

            var units = await _mediator.Send(new SearchUnit.Command(searchUnitDto, userParams));
            
            Response.AddPaginationHeader(units.CurrentPage, units.PageSize, units.TotalCount, units.TotalPages);    
            return Ok(units);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUnit(int id, UnitUpdateDto carUpdateDto)
        {
            var car = await _mediator.Send(new UpdateUnit.Command(id, carUpdateDto));

            if(car != null){
                return Ok(car);
            }
            return BadRequest("Failed to create car");
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost]
        public async Task<ActionResult<UnitDto>> CreateCar(UnitDto unitDto)
        {
            var car = await _mediator.Send(new CreateUnit.Command(unitDto));

            if(car != null){
                return Ok(car);
            }
            return BadRequest("Failed to create blogPost");

        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPut("{id:int}/delete")]
        public async Task<ActionResult> DeleteUnit(int id)
        {
            var car = await _context.Units.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Units.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}