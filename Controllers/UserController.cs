using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Models;
using Bookstore_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService _service;
        public IAuthService _authService;

        public UserController(IUserService service,IAuthService authService)
        {
            _service=service;
            _authService=authService;
        }

		[HttpGet("GetById/{id}")]
        [AllowAnonymous]
		public IActionResult GetById(int id)
		{
			var result = _service.Get(id);
			return Ok(result);
		}

        [HttpGet("GetAll")]
        [AllowAnonymous]
		public IActionResult GetAll()
		{
			var result = _service.GetAll();
			return Ok(result);
		}


        [HttpPut("AddUser")]
        [AllowAnonymous]
        public IActionResult AddUser(User entity){
            
            if (entity == null){
                return BadRequest("User is null.");
            }

            _service.Insert(entity);

            return CreatedAtAction("AddUser", new { id = entity.FirstName }, entity);
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(int id){
            
            _service.Delete(id);

            return Ok();
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public IActionResult UpdateUser(User entity){
            

            if (entity == null){
                return BadRequest("Book is null.");
            }

            _service.Update(entity);

            return Ok();
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public IActionResult LoginUser(UserLoginRequest request){
            

            var result = _authService.LoginUser(request);

            if(result.AuthenticateResult){
                return Ok(result);
            }
            else{
                return Unauthorized(result);
            }
        }

    }
}