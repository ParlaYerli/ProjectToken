using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Domain.Responses;
using Web.API.Domain.Services.Abstract;
using Web.API.Extensions;
using Web.API.Resources;

namespace Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public LoginController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                AccessTokenResponse accessTokenResponse = _service.CreateAccessToken(loginResource.Email, loginResource.Password);
                if (accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.accessToken);
                }
                else
                {
                    return BadRequest(accessTokenResponse.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            AccessTokenResponse accessTokenResponse=_service.CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }
        [HttpPost]
        public IActionResult RevokeRefreshToken(TokenResource tokenResource)
        {
            AccessTokenResponse acccessTokenResponse =_service.RevokeRefreshToken(tokenResource.RefreshToken);
            if (acccessTokenResponse.Success)
            {
                return Ok(acccessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(acccessTokenResponse.Message);
            }
        }

    }
}
