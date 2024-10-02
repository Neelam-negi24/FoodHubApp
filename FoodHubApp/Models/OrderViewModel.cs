using System.ComponentModel.DataAnnotations;

namespace FoodHubApp.Models
{
    public class OrderViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public PaymentViewModel Payment { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }

    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Payment method is required.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Card Number is required.")]
        [CreditCard(ErrorMessage = "Invalid Credit Card Number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card Holder Name is required.")]
        public string CardHolderName { get; set; }
    }
}
