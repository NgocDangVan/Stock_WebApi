using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWebApi.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        
        [ForeignKey("Stock")]
        [Column("stock_id")]
        public int StockId { get; set; }

        [Required(ErrorMessage = "Order Type is require")]
        [Column("order_type")]
        public string OrderType { get; set; }

        [Required(ErrorMessage = "Direction is require")]
        [Column("direction")]
        public string Direction { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity is must be larger than 0")]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status is require")]
        [Column("status")]
        public string Status { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }
        public User? User { get; set; }
        public virtual Stock? Stock { get; set; }
    }
}
