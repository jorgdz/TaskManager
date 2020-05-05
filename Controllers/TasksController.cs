using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.CustomExceptions;
using TaskManager.Models;

namespace TaskManager.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class TasksController : ControllerBase
  {
      private readonly AppDbContext _context; 

      public TasksController(AppDbContext context)
      {
          this._context = context;
      }    

      [HttpGet] 
      public ActionResult<IEnumerable<Models.Task>> GetAll() 
      {     
        return _context.Tasks
          .ToList();
      } 
       
      [HttpGet]
      [Route("{id}")]
      public async Task<ActionResult<Models.Task>> GetById(long id) 
      {    
        var item = await _context.Tasks.FindAsync(id);     
        if (item == null)    
        {         
          throw new NotFoundException("No se ha encontrado la tarea con Id " + id);
        }     
        return Ok(item); 
      }

      [HttpPost]
      public async Task<ActionResult<Models.Task>> Post(Models.Task task)
      {
          _context.Tasks.Add(task);
          await _context.SaveChangesAsync();

          return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
      }
  }

}