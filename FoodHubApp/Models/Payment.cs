namespace FoodHubApp.Models
{
	public class Payment
	{
		public int Id { get; set; }        
		public string PaymentMethod { get; set; }  
		public string CardNumber { get; set; }  
		public string CardHolderName { get; set; }
		public DateTime PaymentDate { get; set; }   
		public decimal Amount { get; set; }   
	}

}
