using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Features.BlogPosts.Commands;
using API.Features.Cards.Commands;
using API.Features.Cards.Queries;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [Authorize]
    public class CardsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public readonly IMediator _mediator;

        private readonly IUserRepository _userRepository;
        public CardsController(IUserRepository userRepository, IMapper mapper,
            IMediator mediator, IPhotoService photoService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberUpdateDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            userParams.CurrentEmail = User.GetEmail();
            var users = await _userRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpGet("agents")]
        public async Task<ActionResult<IEnumerable<MemberUpdateDto>>> GetAgents([FromQuery] UserParams userParams)
        {
            var agents = await _userManager.GetUsersInRoleAsync("Agent");
            _mapper.Map<IEnumerable<MemberUpdateDto>>(agents);
            return Ok(agents);

        }

        // [HttpGet("{id}", Name= "GetUser")]
        // public async Task<ActionResult<MemberUpdateDto>> GetUser(int id)
        // {
        //     // var user = await _userRepository.GetUserByUsernameAsync(username);
        //     // return _mapper.Map<MemberUpdateDto>(user);

        //     return await _userRepository.GetMemberAsync(id);
        // }

        [HttpGet("{email}", Name = "GetUserByEmail")]
        public async Task<ActionResult<MemberDto>> GetUser(string email)
        {
            // var user = await _userRepository.GetUserByUsernameAsync(username);
            // return _mapper.Map<MemberUpdateDto>(user);

            return await _userRepository.GetMemberAgentAsync(email);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _mediator.Send(new UpdateMember.Command(User.GetEmail(), memberUpdateDto));

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-card")]
        public async Task<ActionResult<CardDto>> AddCard(CardDto cardDto)
        {
            var userId = User.GetUserId(); // Get the logged-in user's ID from claims

            if (userId == 0)
            {
                return Unauthorized("User ID could not be retrieved from the token.");
            }

            var card = await _mediator.Send(new CreateCard.Command(cardDto, userId));

            if (card == null)
            {
                return BadRequest("Failed to create card.");
            }

            return Ok(card);
        }


        [HttpGet("cards")]
        public async Task<ActionResult<List<Card>>> GetUserCards()
        {
            var userId = User.GetUserId(); // Get the logged-in user's ID
            var cards = await _mediator.Send(new GetCards.Query(userId));

            if (cards == null || cards.Count == 0)
            {
                return NotFound("No cards found for the user.");
            }

            return Ok(cards);
        }
    }
}