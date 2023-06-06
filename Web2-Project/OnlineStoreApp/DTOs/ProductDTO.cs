using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApp.DTOs
{
    public class ProductDTO : CreateProductDTO
    {
        [Required]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public SellerDTO? Seller { get; set; }
    }
}
