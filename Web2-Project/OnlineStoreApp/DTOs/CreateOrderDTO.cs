using OnlineStoreApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class CreateOrderDTO
    {
        [MaxLength(100)]
        public string? DeliveryAddress { get; set; }
        public string? Comment { get; set; }
        public List<CreateItemDTO>? Items { get; set; }
    }
}
