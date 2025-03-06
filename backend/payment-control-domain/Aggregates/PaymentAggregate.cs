using payment_control_domain.Exceptions;
using payment_control_domain.Entities;
using payment_control_domain.Enums;

namespace payment_control_domain.Aggregates;

public class PaymentAggregate
{
    public PaymentAggregate(int clientId, decimal value, DateTime date, ClientEntity client)
    {
        this.ClientId = clientId;
        this.Value = value;
        this.Date = date;
        this.Status = StatusPaymentEnum.Pending;
        this.Client = client;

        this.IsValid();
    }

    public PaymentAggregate(int id, int clientId, decimal value, DateTime date, StatusPaymentEnum status, ClientEntity client)
    {
        if (id <= 0)
        {
            throw new ValidationEntityException("Id deve ser maior que 0");
        }

        this.Id = id;
        this.ClientId = clientId;
        this.Value = value;
        this.Date = date;
        this.Status = status;
        this.Client = client;

        this.IsValid();
    }

    public void ChangeStatus(StatusPaymentEnum status)
    {
        if (status != StatusPaymentEnum.Pending)
        {
            throw new ValidationEntityException("Status só pode ser alterado se estiver como pendente");
        }

        this.Status = status;
    }

    private void IsValid()
    {
        if (this.ClientId <= 0 || this.Client is null)
        {
            throw new ValidationEntityException("Cliente é necessário para criar o pagamento");
        }

        if (this.Value <= 0)
        {
            throw new ValidationEntityException("O valor deve ser superior a 0 reais");
        }

        if (this.Date == DateTime.MinValue)
        {
            throw new ValidationEntityException("Data é necessária para criar o pagamento");
        }
    }

    public int Id { get; private set; }
    public int ClientId { get; private set; }
    public decimal Value { get; private set; }
    public DateTime Date { get; private set; }
    public StatusPaymentEnum Status { get; private set; }
    public ClientEntity Client { get; private set; }
}