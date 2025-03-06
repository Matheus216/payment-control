using payment_control_application.Models.Client;
using payment_control_application.Models;
using payment_control_domain.Entities;

namespace payment_control_application.Services.Client;
public interface IClientService
{
    Task<Result<IEnumerable<ClientResponse>>> GetAll(ClientRequest request);
    Task<Result<ClientResponse>> Create(ClientCreateRequest request);
}