using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uroboro.BL.Managers;
using Uroboro.Common.Models;

namespace Uroboro.SL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : Controller
    {
        // Context management moved to its own Assembly
        // private readonly TodoContext _context;
        private readonly IBaseManager<TodoItem> _manager;

        // Context management moved to its own Assembly
        //public TodoItemsController(TodoContext context)
        //{
        //    _context = context;
        //    _manager = new TodoItemsManager();
        //}

        public TodoItemsController(IBaseManager<TodoItem> manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            // var todoItems = await _context.TodoItems.ToListAsync();
            var todoItems = await _manager.Read();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            //var todoItem = await _context.TodoItems.FirstOrDefaultAsync(m => m.Id == id);
            var todoItem = await _manager.Details(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TodoItem>> Create([Bind("Id,Name,IsComplete")] TodoItem todoItem)
        {
            if (todoItem == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            //_context.TodoItems.Add(todoItem);
            //await _context.SaveChangesAsync();
            var result = await _manager.Create(todoItem);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(long id, [Bind("Id,Name,IsComplete")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(todoItem);
                    //await _context.SaveChangesAsync();
                    var result = await _manager.Update(todoItem);
                    if (result == null)
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex);
                }
            }
            return Ok(todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            //var todoItem = await _context.TodoItems.FindAsync(id);
            //_context.TodoItems.Remove(todoItem);
            //await _context.SaveChangesAsync();
            var result = await _manager.Delete(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}