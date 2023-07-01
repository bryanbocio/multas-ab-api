using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BasketItemsDto
    {
        [Required]

        public int Id { get; set; }

        [Required]
        public int TrafficFineId { get; set; }

        [Required]
        [Range(0.10, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal TrafficFinePrice { get; set; }
    }
}
