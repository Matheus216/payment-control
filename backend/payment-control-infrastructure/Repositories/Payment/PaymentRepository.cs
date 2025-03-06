using payment_control_domain.Interfaces.Repositories;
using payment_control_infrastructure.Context;
using payment_control_infrastructure.Models;
using payment_control_domain.Aggregates;
using payment_control_domain.Entities;
using Microsoft.EntityFrameworkCore;
using payment_control_domain.Enums;

namespace payment_control_infrastructure.Repositories.Payment;

public class PaymentRepository : IPaymentRepository
{
    private readonly DbSet<PaymentModel> _payments;
    private readonly PaymentContext _context;

    public PaymentRepository(PaymentContext context)
    {
        _context = context;
        _payments = context.Payments;
    }

    public async Task<int> Create(PaymentAggregate aggregate)
    {
        var model = new PaymentModel
        {
            ClientId = aggregate.ClientId,
            Date = aggregate.Date,
            Value = aggregate.Value,
            Status = (int)aggregate.Status
        };

        var insered = await _payments.AddAsync(model);
        await _context.SaveChangesAsync();

        return insered.Entity.Id;
    }

    public async Task<IEnumerable<PaymentAggregate>> GetByClientId(int clientId)
    {
        var response = await _payments
            .AsNoTracking()
            .Where(x => x.ClientId == clientId)
            .Include(x => x.Client)
            .ToListAsync();
        
        return response.Select(x => new PaymentAggregate(
            x.Id,
            x.ClientId,
            x.Value,
            x.Date,
            (StatusPaymentEnum)x.Status,
            new ClientEntity(x?.Client?.Name ?? "", x?.Client?.Email ?? "")
        ));
    }

    public async Task<PaymentAggregate> GetById(int id)
    {
        var model = await _payments
            .AsNoTracking()
            .Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (model is null)
            throw new Exception("Não encontrado dados na base");

        return new PaymentAggregate(
            model.Id,
            model.ClientId,
            model.Value,
            model.Date,
            (StatusPaymentEnum)model.Status,
            new ClientEntity(model.Client.Name, model.Client.Email)
        );
    }

    public async Task<int> Update(PaymentAggregate aggregate)
    {
        var model = new PaymentModel
        {
            Id = aggregate.Id,
            ClientId = aggregate.ClientId,
            Date = aggregate.Date,
            Value = aggregate.Value,
            Status = (int)aggregate.Status
        };

        _payments.Update(model);
        var linesAffected = await _context.SaveChangesAsync();

        return linesAffected;
    }

}
