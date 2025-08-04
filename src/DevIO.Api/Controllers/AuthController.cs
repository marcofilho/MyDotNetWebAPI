using DevIO.Api.Dtos;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DevIO.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(INotificator notificator,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager) : base(notificator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("new-account")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return CustomResponse(registerUser);
            }

            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(loginUser);
            }

            if (result.IsLockedOut)
            {
                NotifyError("User temporarily blocked.");
                return CustomResponse(loginUser);
            }

            NotifyError("Invalid email or password.");
            return CustomResponse(loginUser);
        }
    }
}