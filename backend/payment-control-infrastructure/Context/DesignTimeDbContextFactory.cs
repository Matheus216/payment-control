using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace payment_control_infrastructure.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PaymentContext>
{
    public PaymentContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<PaymentContext>();

        options.UseSqlite("Data Source=../payment-control-api/App_Data/paymentcontrol.db");

        return new PaymentContext(options.Options);
    }
}
