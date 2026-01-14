namespace ExpensesTracker.API.Contracts.Responses
{
    public class SnapshotResponse
    {
        public Guid Id { get; set; }

        //public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
