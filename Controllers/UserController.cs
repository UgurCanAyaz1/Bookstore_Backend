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
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _service.GetAsync(id);
			return Ok(result);
		}

        [HttpGet("GetAll")]
        [AllowAnonymous]
		public async Task<IActionResult> GetAll()
		{
			var result = await _service.GetAllAsync();
			return Ok(result);
		}


        [HttpPut("AddUser")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(User entity){
            
            if (entity == null){
                return BadRequest("User is null.");
            }
            await _service.InsertAsync(entity);

            return CreatedAtAction("AddUser", new { id = entity.FirstName }, entity);
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUser(int id){
            
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(User entity){
            

            if (entity == null){
                return BadRequest("Book is null.");
            }

            await _service.UpdateAsync(entity);

            return Ok();
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(UserLoginRequest request){
            

            var result = await _authService.LoginUser(request);

            if(result.AuthenticateResult){
                return Ok(result);
            }
            else{
                return Unauthorized(result);
            }
        }

    }
}