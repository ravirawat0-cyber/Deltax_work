namespace Assignment3.Models.DbModels
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
