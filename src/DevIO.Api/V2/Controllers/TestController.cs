using DevIO.Api.Controllers;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V2.Controllers
{
    [Authorize]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    public class TestController : MainController
    {
        private readonly IUser _appUser;
        private readonly ILogger _logger;

        public TestController(INotificator notificator, IUser appUser, ILogger<TestController> logger) : base(notificator, appUser)
        {
            _appUser = appUser;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogTrace("Trace Log.");
            _logger.LogDebug("Debug Log.");
            _logger.LogInformation("Information Log.");
            _logger.LogWarning("Warning Log.");
            _logger.LogError("Error Log.");
            _logger.LogCritical("Critical Error Log.");

            var userId = _appUser.GetUserId();
            var name = _appUser.Name;
            var userEmail = _appUser.GetUserEmail();
            var result = new
            {
                userId,
                name,
                userEmail,
                LoggedUser = _appUser.IsAuthenticated()
            };

            return CustomResponse(result);
        }
    }
}
