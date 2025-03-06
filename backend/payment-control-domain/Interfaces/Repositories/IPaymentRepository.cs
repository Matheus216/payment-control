using payment_control_domain.Aggregates;

namespace payment_control_domain.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<int> Create(PaymentAggregate aggregate);
    Task<int> Update(PaymentAggregate aggregate);
    Task<IEnumerable<PaymentAggregate>> GetByClientId(int clientId);
    Task<PaymentAggregate> GetById(int id);
}
