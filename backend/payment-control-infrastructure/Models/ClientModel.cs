namespace payment_control_infrastructure.Models;

public class ClientModel
{
    public int Id { get; set; }   
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<PaymentModel> Payments { get; set; } 
}
