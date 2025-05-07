namespace CompanyExpenseTracking.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string? IBAN { get; set; } // Nullable hale getirildi
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>(); // Koleksiyon başlatıldı
    }
}