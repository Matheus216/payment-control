using payment_control_domain.Entities;

namespace payment_control_domain.Interfaces.Repositories;

public interface IClientRepository
{
    Task<int> Create(ClientEntity entity);
    Task<IEnumerable<ClientEntity>> GetAll(int page = 1, int itemsPeerPage = 50);
    Task<ClientEntity> GetById(int id);
    Task<int> GetTotal();
}
