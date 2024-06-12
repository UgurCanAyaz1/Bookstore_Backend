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
    public class BookController : ControllerBase
    {
        public IBookService _service;

        public BookController(IBookService service)
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

        [HttpPut("AddBook")]
        public IActionResult AddBook(Book entity){
            
            if (entity == null){
                return BadRequest("Book is null.");
            }

            _service.Insert(entity);

            return CreatedAtAction("AddBook", new { id = entity.Name }, entity);
        }

        // TODO: add delete and update endpoints
    }
}