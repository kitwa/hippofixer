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
        public readonly IMediator _mediator;

        private readonly IUserRepository _userRepository;
        public CardsController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
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


        [HttpGet("get-cards")]
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