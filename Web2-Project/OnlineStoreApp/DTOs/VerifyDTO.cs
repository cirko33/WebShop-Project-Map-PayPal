using OnlineStoreApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class VerifyDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public VerificationStatus VerificationStatus { get; set; }
    }
}
