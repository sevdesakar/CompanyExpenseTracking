using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient; 


namespace CompanyExpenseTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Raporlama işlemleri sadece Admin tarafından yapılabilir
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        }

        // GET: api/Reports/PersonalExpenses/{userId}
        [HttpGet("PersonalExpenses/{userId}")]
        public async Task<IActionResult> GetPersonalExpenses(int userId)
        {
            using var connection = CreateConnection();
            var query = @"
                SELECT e.Id, e.Description, e.Amount, e.Date, e.Status, e.RejectionReason, c.Name AS CategoryName
                FROM Expenses e
                INNER JOIN Categories c ON e.CategoryId = c.Id
                WHERE e.UserId = @UserId";

            var expenses = await connection.QueryAsync(query, new { UserId = userId });
            return Ok(expenses);
        }

        // GET: api/Reports/CompanyExpenses
        [HttpGet("CompanyExpenses")]
        public async Task<IActionResult> GetCompanyExpenses()
        {
            using var connection = CreateConnection();
            var query = @"
                SELECT 
                    FORMAT(e.Date, 'yyyy-MM-dd') AS Date,
                    SUM(CASE WHEN e.Status = 'Approved' THEN e.Amount ELSE 0 END) AS ApprovedAmount,
                    SUM(CASE WHEN e.Status = 'Rejected' THEN e.Amount ELSE 0 END) AS RejectedAmount,
                    COUNT(*) AS TotalExpenses
                FROM Expenses e
                GROUP BY FORMAT(e.Date, 'yyyy-MM-dd')
                ORDER BY Date";

            var report = await connection.QueryAsync(query);
            return Ok(report);
        }

        // GET: api/Reports/PersonnelSpending
        [HttpGet("PersonnelSpending")]
        public async Task<IActionResult> GetPersonnelSpending()
        {
            using var connection = CreateConnection();
            var query = @"
                SELECT u.Username, SUM(e.Amount) AS TotalSpending
                FROM Expenses e
                INNER JOIN Users u ON e.UserId = u.Id
                WHERE e.Status = 'Approved'
                GROUP BY u.Username
                ORDER BY TotalSpending DESC";

            var report = await connection.QueryAsync(query);
            return Ok(report);
        }
    }
}
