using payment_control_domain.Interfaces.Repositories;
using payment_control_application.Services.Client;
using payment_control_application.Models.Payment;
using payment_control_application.Models;
using payment_control_domain.Exceptions;
using payment_control_domain.Aggregates;
using Microsoft.Extensions.Logging;
using payment_control_domain.Enums;

namespace payment_control_application.Services.Payment;
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository; 
    private readonly IClientService _clientService;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(
        IPaymentRepository repository, IClientService clientService, 
        ILogger<PaymentService> logger
    )
    {
        _logger = logger;
        _repository = repository;
        _clientService = clientService;
    }
    
    public async Task<Result<PaymentResponse>> Create(PaymentRequest request)
    {
        try 
        {
            var client = await _clientService.GetById(request.ClientId);

            if (client is null || !client.Success)
                return new("Cliente não encontrado na base", CodReturn.BadRequest);

            var entity = new PaymentAggregate(request.ClientId, request.Value, request.Date, client.Data);

            var result = await _repository.Create(entity);

            return new
            (
                new PaymentResponse 
                { 
                    Id = result, 
                    Value = request.Value, 
                    Date = request.Date, 
                    ClientId = request.ClientId, 
                    Status = StatusPaymentEnum.Pending 
                }
            );
        }
        catch (ValidationEntityException ex)
        {
            _logger.LogWarning("Erro na validação da entidade");
            return new(ex.Message, CodReturn.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new("Erro não esperado");
        }
    }

    public async Task<Result<IEnumerable<PaymentResponse>>> GetByClientId(int clientId)
    {
        try 
        {
            var response = await _repository.GetByClientId(clientId);

            return new(response.Select(x => new PaymentResponse
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Value = x.Value,
                Date = x.Date,
                Status = (StatusPaymentEnum)x.Status
            }));
        }
        catch (ValidationEntityException ex)
        {
            _logger.LogWarning("Erro na validação da entidade");
            return new(ex.Message, CodReturn.BadRequest);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new("Erro não esperado");
        }
    }

    public async Task<Result<PaymentResponse>> UpdateStatus(int paymentId, StatusPaymentEnum status)
    {
        try 
        {
            _logger.LogInformation("Atualizando status do pagamento");
            var payment = await _repository.GetById(paymentId);

            if (payment is null || payment.Id == 0)
                return new("Pagamento não encontrado", CodReturn.NotFound);

            payment.ChangeStatus(status);

            var result = await _repository.Update(payment);

            return new(new PaymentResponse 
            { 
                Id = payment.Id, 
                ClientId = payment.ClientId, 
                Value = payment.Value, 
                Date = payment.Date, 
                Status = payment.Status 
            });
        }
        catch (ValidationEntityException ex)
        {
            _logger.LogWarning("Erro na validação da entidade");
            return new(ex.Message, CodReturn.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new("Erro não esperado");
        }
    }

    public async Task<Result<SummaryResponse>> GetSummary()
    {
        try 
        {
            var countedPending = await _repository.GetTotalByStatus(StatusPaymentEnum.Pending);
            var countedPaid = await _repository.GetTotalByStatus(StatusPaymentEnum.Paid);
            var countedCanceled = await _repository.GetTotalByStatus(StatusPaymentEnum.Canceled);
            var countedClient = await _clientService.GetCount();

            return new(new SummaryResponse {
                TotalPending = countedPending,
                TotalPaid = countedPaid,
                TotalCanceled = countedCanceled,
                TotalClients = countedClient
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ("Erro não esperado");
        }
    }
}