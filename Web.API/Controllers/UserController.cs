using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Domain.Model;
using Web.API.Domain.Responses;
using Web.API.Domain.Services;
using Web.API.Domain.Services.Abstract;
using Web.API.Resources;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;
            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
            UserResponse userResponse = _service.FindById(int.Parse(userId));
            if (userResponse.Success)
            {
                return Ok(userResponse.User);
            }
            else
            {
                return BadRequest(userResponse.User);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = _mapper.Map<UserResource, User>(userResource);
            UserResponse userResponse = _service.AddUser(user);
            if (userResponse.Success)
            {
                return Ok(userResponse.User);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }
    }
}
