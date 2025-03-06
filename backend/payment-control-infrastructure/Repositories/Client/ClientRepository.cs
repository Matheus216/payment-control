using Microsoft.EntityFrameworkCore;
using payment_control_domain.Entities;
using payment_control_domain.Interfaces.Repositories;
using payment_control_infrastructure.Context;
using payment_control_infrastructure.Models;

namespace payment_control_infrastructure.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly DbSet<ClientModel> _clients; 
    private readonly PaymentContext _context;

    public ClientRepository(PaymentContext context)
    {
        _context = context;
        _clients = context.Clients;
    }

    public async Task<int> Create(ClientEntity entity)
    {
        var model = new ClientModel
        {
            Name = entity.Name,
            Email = entity.Email
        };

        await _clients.AddAsync(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ClientEntity>> GetAll(int page = 1, int itemsPeerPage = 50)
    {
        return await _clients
            .OrderBy(x => x.Id)
            .Skip(page * (itemsPeerPage - 1))
            .Take(itemsPeerPage)
            .Select(x => new ClientEntity(
            
                x.Id,
                x.Name,
                x.Email
            ))
            .ToListAsync();
    }
}
