using OnlineStoreApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class CreateOrderDTO
    {
        [Required, MaxLength(100)]
        public string? DeliveryAddress { get; set; }
        [Required]
        public double? PositionX { get; set; }
        [Required]
        public double? PositionY { get; set; }
        public string? Comment { get; set; }
        public List<CreateItemDTO>? Items { get; set; }
    }
}
