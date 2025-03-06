using payment_control_application.Models.Client;
using payment_control_application.Models;

namespace payment_control_application.Services.Client;
public interface IClientService
{
    Task<Result<IEnumerable<ClientResponse>>> GetAll(ClientRequest request);
    Task<Result<ClientResponse>> Create(ClientCreateRequest request);
    Task<Result<ClientResponse>> GetById(int id);
}