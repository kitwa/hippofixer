using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Constants;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Features.Users.Commands;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await EmailExists(registerDto.Email)) return BadRequest("Cette adresse e-mail est prise");

            registerDto.CityId = 1;
            registerDto.CountryId = 1;
            registerDto.GenderId = 1;

            var user = _mapper.Map<AppUser>(registerDto);

            user.Email = registerDto.Email.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResult =  await _userManager.AddToRoleAsync(user, "Member"); 

            if(!roleResult.Succeeded) return BadRequest(result.Errors);

            var sendEmail = new SendEmail();
            await sendEmail.SendWelcomeEmailAsync(registerDto.Email, registerDto.Username);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Id = user.Id
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Vous avez entré un mauvais email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized("Vous avez entré un mauvais mot de passe");

            return new UserDto
            {
                Email = user.Email,
                Token = await  _tokenService.CreateToken(user),
                Id = user.Id
            };
        }

        
        [HttpPost("reset-password/{email}")]
        public async Task<ActionResult<string>> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email.ToLower());

            // if (user == null) return Unauthorized("l'adresse email n'existe pas.");
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return BadRequest("Nous ne trouvons pas de compte associé à cette adresse e-mail.");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // // Save token to user entity
            // user.ResetToken = resetToken;
            // await _userManager.UpdateAsync(user);

            var callbackUrl = $"{Request.Scheme}://{Request.Host}/newpassword?email={email}&resetToken={resetToken}";

            var sendEmail = new SendEmail();
            if(await sendEmail.SendResetPasswordEmailAsync(user.Email, user.UserName, callbackUrl)) 
                return NoContent();
            else
                return BadRequest("Nous n'avons pas pu envoyer d'e-mail, veuillez réessayer.");
   
        }

        [HttpPost("new-password")]
        public async Task<ActionResult<UserDto>> NewPassword(NewPasswordDto newPasswordDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == newPasswordDto.Email.ToLower());
             var user2 = await _userManager.FindByIdAsync(user.Id.ToString());

            if (user == null) return Unauthorized("Vous avez entré un mauvais email ou votre lien a expiré.");

            // user.ResetToken = null;
            var result = await _userManager.ResetPasswordAsync(user, newPasswordDto.ResetToken, newPasswordDto.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);


            return NoContent();
        }

        private async Task<bool> EmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

    }
}