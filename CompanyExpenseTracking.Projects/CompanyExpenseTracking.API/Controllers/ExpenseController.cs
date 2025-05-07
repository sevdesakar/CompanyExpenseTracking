using CompanyExpenseTracking.Domain.Entities;
using CompanyExpenseTracking.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyExpenseTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Tüm endpoint'ler için yetkilendirme
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (currentUserRole == "Personel")
            {
                var personalExpenses = await _context.Expenses
                    .Where(e => e.UserId == currentUserId)
                    .ToListAsync();
                return Ok(personalExpenses);
            }

            var allExpenses = await _context.Expenses.ToListAsync();
            return Ok(allExpenses);
        }

        // POST: api/Expense
        [HttpPost]
        [Authorize(Roles = "Personel")]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            expense.UserId = currentUserId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpenses), new { id = expense.Id }, expense);
        }

        // PUT: api/Expense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
        {
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (currentUserRole == "Personel" && expense.UserId != currentUserId)
                return Forbid("Personel sadece kendi masraflarını güncelleyebilir.");

            if (id != expense.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Expenses.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Expense/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
