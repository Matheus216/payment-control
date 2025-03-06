using payment_control_application.Models;
using payment_control_application.Models.Client;
using payment_control_domain.Entities;
using payment_control_domain.Interfaces.Repositories;

namespace payment_control_application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<ClientResponse>> Create(ClientCreateRequest request)
    {
        var entity = new ClientEntity(request.Name, request.Email); 

        var result = await _repository.Create(entity);

        return new(new ClientResponse { Id = result });
    }

    public async Task<Result<IEnumerable<ClientResponse>>> GetAll(ClientRequest request)
    {
        var response = await _repository.GetAll
        (
            request.Pagination.Page, 
            request.Pagination.PageSize
        );

        return new(response.Select(x => new ClientResponse
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email
        }));
    }
}