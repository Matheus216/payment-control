using payment_control_domain.Enums;

namespace payment_control_application.Models.Payment;

public class PaymentResponse : PaymentAbstract
{
    public int Id { get; set; }
    public StatusPaymentEnum Status { get; set; }
}
