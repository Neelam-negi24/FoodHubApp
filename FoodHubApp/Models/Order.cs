using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHubApp.Models
{
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal? TotalAmount => Price * Quantity;

	
		public int PaymentId { get; set; }    
		public Payment Payment { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
