using payment_control_domain.Interfaces.Repositories;
using payment_control_application.Models.Client;
using payment_control_application.Models;
using payment_control_domain.Exceptions;
using payment_control_domain.Entities;
using Microsoft.Extensions.Logging;

namespace payment_control_application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly ILogger _logger;

    public ClientService(IClientRepository repository, ILogger<ClientService> logger)
    {
        _repository = repository;
        _logger =  logger;
    }
    
    public async Task<Result<ClientResponse>> Create(ClientCreateRequest request)
    {
        try
        {
            var entity = new ClientEntity(request.Name, request.Email);

            var result = await _repository.Create(entity);

            return new(new ClientResponse { Id = result });
        }
        catch (ValidationEntityException ex)
        {
            _logger.LogWarning("Erro na validação da entidade"); 
            return new(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new("Erro não esperado");
        }
    }

    public async Task<Result<IEnumerable<ClientResponse>>> GetAll(ClientRequest request)
    {
        try
        {
            var response = await _repository.GetAll
           (
               request.Pagination.Page,
               request.Pagination.PageSize
           );

            return new
            (
                response.Select(x => new ClientResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email
                })
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

    public async Task<Result<ClientResponse>> GetById(int id)
    {
        try
        {
            var response = await _repository.GetById(id);

            return new(new ClientResponse
            {
                Id = response.Id,
                Name = response.Name,
                Email = response.Email
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
}