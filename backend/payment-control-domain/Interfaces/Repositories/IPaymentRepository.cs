using payment_control_domain.Aggregates;
using payment_control_domain.Enums;

namespace payment_control_domain.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<int> Create(PaymentAggregate aggregate);
    Task<int> Update(PaymentAggregate aggregate);
    Task<IEnumerable<PaymentAggregate>> GetByClientId(int clientId);
    Task<PaymentAggregate> GetById(int id);
    Task<int> GetTotalByStatus(StatusPaymentEnum status);
}
