using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Order.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        [NotMapped] public decimal TotalPrice => OrderItems.Sum(x => x.Price);
        public List<OrderItem> OrderItems { get; set; } = default!;
    }


    public record OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public decimal Price { get; set; }
        public string OrderId { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}