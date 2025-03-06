
namespace payment_control_infrastructure.Models;

public class PaymentModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
    public ClientModel? Client { get; set; }
}
