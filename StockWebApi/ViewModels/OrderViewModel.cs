using StockWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockWebApi.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int StockId { get; set; }
        [Required(ErrorMessage = "Order Type is require")]
        public string OrderType { get; set; }
        [Required(ErrorMessage = "Direction is require")]
        public string Direction { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity is must be larger than 0")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Status is require")]
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
