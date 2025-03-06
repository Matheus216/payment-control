using payment_control_application.Models;
using payment_control_application.Models.Client;

namespace payment_control_application.Services.Client;

public class ClientService : IClientService
{
    public Task<Result<ClientResponse>> Create(ClientCreateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ClientResponse>> GetAll(ClientRequest request)
    {
        throw new NotImplementedException();
    }
}