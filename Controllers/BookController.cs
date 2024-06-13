using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        public IBookService _service;

        public BookController(IBookService service)
        {
            _service=service;
        }

        [HttpGet("GetById{id}")]
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

        [HttpPut("AddBook")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddBook(Book entity){
            
            if (entity == null){
                return BadRequest("Book is null.");
            }

            _service.Insert(entity);

            return CreatedAtAction("AddBook", new { id = entity.Name }, entity);
        }

        [HttpDelete("DeleteBook/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteBook(int id){
            
            _service.Delete(id);

            return Ok();
        }

        [HttpPut("UpdateBook")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult UpdateBook(Book entity){
            

            if (entity == null){
                return BadRequest("Book is null.");
            }

            _service.Update(entity);

            return Ok();
        }
    }
}