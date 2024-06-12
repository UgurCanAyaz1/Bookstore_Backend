using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService _service;
        public UserController(IUserService service)
        {
            _service=service;
        }

		[HttpGet("GetById")]
		public IActionResult GetById(int id)
		{
			var result = _service.Get(id);
			return Ok(result);
		}

        [HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _service.GetAll();
			return Ok(result);
		}


        [HttpPut("AddUser")]
        public IActionResult AddUser(User entity){
            
            if (entity == null){
                return BadRequest("User is null.");
            }

            _service.Insert(entity);

            return CreatedAtAction("AddUser", new { id = entity.FirstName }, entity);
        }

        // TODO: add delete and update endpoints

    }
}