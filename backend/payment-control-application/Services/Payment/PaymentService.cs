using payment_control_application.Models;
using payment_control_application.Models.Payment;
using payment_control_domain.Enums;

namespace payment_control_application.Services.Payment;
public class PaymentService : IPaymentService
{
    public Task<Result<PaymentResponse>> Create(PaymentRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<PaymentResponse>>> GetByClientId(int clientId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PaymentResponse>> UpdateStatus(int paymentId, StatusPaymentEnum status)
    {
        throw new NotImplementedException();
    }
}