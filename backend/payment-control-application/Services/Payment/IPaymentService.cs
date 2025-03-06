using payment_control_application.Models.Payment;
using payment_control_application.Models;
using payment_control_domain.Enums;

namespace payment_control_application.Services.Payment;

public interface IPaymentService
{
    Task<Result<PaymentResponse>> Create(PaymentRequest request);
    Task<Result<IEnumerable<PaymentResponse>>> GetByClientId(int clientId);
    Task<Result<PaymentResponse>> UpdateStatus(int paymentId, StatusPaymentEnum status);
}
