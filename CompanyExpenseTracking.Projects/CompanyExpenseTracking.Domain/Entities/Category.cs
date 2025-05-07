namespace CompanyExpenseTracking.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; } 
    }
}