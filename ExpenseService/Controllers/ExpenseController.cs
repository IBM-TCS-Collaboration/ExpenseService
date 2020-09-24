using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseService.Database;
using ExpenseService.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        DatabaseContext db;

        public ExpenseController()
        {
            db = new DatabaseContext();

        }
        // GET: api/<ExpenseController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExpenseController>/5
        [ActionName("getExpense")]
        [HttpGet("{UserId}")]
        public IEnumerable<expense> Get(int UserId)
        {
            
            var expense = db.Expenses.FromSql("Select * from expenses where UserId={0}", UserId);
            return expense;
        }

        [ActionName("getExpenseDetails")]
        [HttpGet("{ExpenseId}")]
        public IEnumerable<expense> GetExpenseDet(int ExpenseId)
        {

            var expense = db.Expenses.FromSql("Select * from expenses where ExpenseId={0}", ExpenseId);
            return expense;
        }

        // POST api/<ExpenseController>


        [ActionName("CreateExpense")]
        [HttpPost]
        public IActionResult Post([FromBody] expense e)
        {
            try
            {
                db.Expenses.Add(e);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [ActionName("CreateExpenseDocument")]
        [HttpPost]
        public IActionResult Post([FromBody] documents e)
        {
            try
            {
                db.Documents.Add(e);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<ExpenseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExpenseController>/5
        [ActionName("deleteExpenseDetails")]
        [HttpDelete("{ExpenseId}")]
        public IActionResult Delete(int ExpenseId)
        {
            var expenseToDelete = db.Expenses.SingleOrDefault(x => x.ExpenseId == ExpenseId);
            var documentToDelete = db.Documents.SingleOrDefault(x => x.ExpenseId == ExpenseId);


            if (expenseToDelete == null)
            {
                return NotFound("No record found");
            }

            db.Expenses.Remove(expenseToDelete);
            db.Documents.Remove(documentToDelete);
            db.SaveChanges();

            return Ok();
        }
    }
}
