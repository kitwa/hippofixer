using API.Entities;
using API.Features.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        public AdminController(UserManager<AppUser> userManager, IMediator mediator)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
            .Include(r => r.UserRoles)
            .ThenInclude(r => r.Role)
            .OrderBy(u => u.UserName)
            .Select(u => new{
                u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            })
            .ToListAsync();

        return Ok(users);
            
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost("edit-roles/{email}")]
        public async Task<ActionResult> EditRoles(string email, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByEmailAsync(email);

            if(user == null) return NotFound("User with this email could not be found");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if(!result.Succeeded) return BadRequest("Remove from roles has failed");

            return Ok(await _userManager.GetRolesAsync(user));

        }

        [Authorize(Policy = "AgentPhotoRole")]
        [HttpGet("photos-to-agent")]
        public ActionResult GetPhotosFors()
        {
            return Ok("Admin or Agent Access");
        }

        [HttpGet("stats")]
        public async Task<ActionResult> GetStats()
        {
            var stats = await _mediator.Send(new GetStats.Query());
    
            return Ok(stats);
        }
    }
}