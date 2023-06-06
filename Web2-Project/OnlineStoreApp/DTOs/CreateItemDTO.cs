using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class CreateItemDTO
    {
        [Required, Range(0, int.MaxValue)]
        public int Amount { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
