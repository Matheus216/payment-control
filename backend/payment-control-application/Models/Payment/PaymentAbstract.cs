namespace payment_control_application.Models.Payment;

public abstract class PaymentAbstract
{
    public int ClientId { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}
