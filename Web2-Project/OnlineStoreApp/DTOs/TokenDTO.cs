using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class TokenDTO
    {
        [Required]
        public string? Token { get; set; }
    }
}
