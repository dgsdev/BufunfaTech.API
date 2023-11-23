namespace BufunfaTech.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }         
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Title { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string? Note { get; set; }       
    }
}
