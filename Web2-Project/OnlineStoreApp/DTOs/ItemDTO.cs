using OnlineStoreApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class ItemDTO : CreateItemDTO
    {
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}
