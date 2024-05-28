namespace Order.API.Models
{
    public class OutBox
    {
        public int Id { get; set; }

        public string PayloadEvent { get; set; } = default!;
        public string? HeaderEvent { get; set; }

        public DateTime Created { get; set; }

        public string EventType { get; set; } = default!;

        public bool SendBus { get; set; }
    }
}