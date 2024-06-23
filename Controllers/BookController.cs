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

        [HttpPut("AddBook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddBook(Book entity){
            
            if (entity == null){
                return BadRequest("Book is null.");
            }

            await _service.InsertAsync(entity);

            return CreatedAtAction("AddBook", new { id = entity.Name }, entity);
        }

        [HttpDelete("DeleteBook/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteBook(int id){
            
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("UpdateBook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateBook(Book entity){
            

            if (entity == null){
                return BadRequest("Book is null.");
            }

            await _service.UpdateAsync(entity);

            return Ok();
        }
    }
}