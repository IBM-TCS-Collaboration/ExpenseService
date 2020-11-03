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
        
         [ActionName("ExpenseSubmittedCount")]
        [HttpGet("{UserID}")]
        public IActionResult GetExpenseSubmittedCount(int UserID)
        {
            var query = from ex in db.Expenses
                        where ex.UserID == UserID
                        group ex by 1 into a
                        
                        select new
                        {

                            ExpenseSubmittedCount = a.Count()

                        };


            return Ok(query);
        }
             
        [ActionName("ExpenseSubmittedCount")]
        [HttpGet]
        public IActionResult GetExpenseSubmittedCount()
        {
            var query = from expense in db.Expenses
                        group expense by 1 into a

                        select new
                        {

                            ExpenseSubmittedCount = a.Count()

                        };


            return Ok(query);
        }
        
        [ActionName("ExpenseSubmittedApprovedCount")]
        [HttpGet("{UserID}")]
        public IActionResult ExpenseSubmittedApprovedCount(int UserID)
        {
            var query = from ex in db.Expenses
                        where ex.UserID == UserID && ex.ExpenseStatus == 1
                        group ex by 1 into a

                        select new
                        {

                            ExpenseSubmittedCount = a.Count()

                        };


            return Ok(query);
        }

        [ActionName("ExpenseSubmittedRejectedCount")]
        [HttpGet("{UserID}")]
        public IActionResult ExpenseSubmittedRejectedCount(int UserID)
        {
            var query = from ex in db.Expenses
                        where ex.UserID == UserID && ex.ExpenseStatus == 2
                        group ex by 1 into a

                        select new
                        {

                            ExpenseSubmittedCount = a.Count()

                        };


            return Ok(query);
        }

        [ActionName("ExpenseSubmittedApprovedCount")]
        [HttpGet]
        public IActionResult ExpenseSubmittedApprovedCount()
        {
            var query = from ex in db.Expenses
                        where ex.ExpenseStatus == 1
                        group ex by 1 into a

                        select new
                        {

                            ExpenseSubmittedCount = a.Count()

                        };


            return Ok(query);
        }

        [ActionName("ExpenseSubmittedRejectedCount")]
        [HttpGet]
        public IActionResult ExpenseSubmittedRejectedCount()
        {
            var query = from ex in db.Expenses
                        where ex.ExpenseStatus == 2
                        group ex by 1 into a

                        select new
                        {

                            ExpenseSubmittedCount = a.Count()

                        };


            return Ok(query);
        }
        
        [ActionName("ExpenseApproval")]
        [HttpPut("{ExpenseID}")]
        public IActionResult ExpenseApproval([FromBody] expense n)
        {
            var existingExpense = db.Expenses.Where(s => s.ExpenseId == n.ExpenseId).FirstOrDefault<expense>();


            if (existingExpense != null)
            {
                existingExpense.ExpenseStatus = n.ExpenseStatus;
                db.SaveChanges();
                
             }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        
         [ActionName("getAllExpenses")]
        [HttpGet]
        public IEnumerable<expense> getAllExpenses()
        {

            var expense = db.Expenses.FromSql("Select * from Expenses");
            return expense;
        }
        
        [ActionName("getExpenseExportReport")]
        [HttpGet("{StartDate}/{EndDate}")]
        public IEnumerable<expense> getExpenseExportReport(DateTime StartDate, DateTime EndDate)
        {

            var expense = db.Expenses.FromSql("Select * from expensedb.Expenses where FromDate between {0} and {1};", StartDate, EndDate);
            return expense;

        }
        
        
        // GET api/<ExpenseController>/5
        [ActionName("getExpense")]
        [HttpGet("{UserId}")]
        public IEnumerable<expense> Get(int UserId)
        {
            
            var expense = db.Expenses.FromSql("Select * from expensedb.Expenses where UserId={0}", UserId);
            return expense;
        }

        [ActionName("getExpenseDetails")]
        [HttpGet("{ExpenseId}")]
        public IEnumerable<expense> GetExpenseDet(int ExpenseId)
        {

            var expense = db.Expenses.FromSql("Select * from expensedb.Expenses where ExpenseId={0}", ExpenseId);
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
