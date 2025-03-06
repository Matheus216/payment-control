using payment_control_domain.Aggregates;
using payment_control_domain.Entities;
using payment_control_domain.Enums;
using payment_control_domain.Exceptions;

namespace payment_control_tests.Domain.Aggregates;


public class PaymentAggregateTest
{
    [Fact]
    public void Constructor_ShouldCreatePaymentAggregate_WhenValidParameters()
    {
        // Arrange
        int clientId = 1;
        decimal value = 100.0m;
        DateTime date = DateTime.Now;
        var client = new ClientEntity(clientId,"Test Client", "email");

        // Act
        var paymentAggregate = new PaymentAggregate(clientId, value, date, client);

        // Assert
        Assert.Equal(clientId, paymentAggregate.ClientId);
        Assert.Equal(value, paymentAggregate.Value);
        Assert.Equal(date, paymentAggregate.Date);
        Assert.Equal(StatusPaymentEnum.Pending, paymentAggregate.Status);
        Assert.Equal(client, paymentAggregate.Client);
    }

    [Fact]
    public void Constructor_ShouldThrowValidationEntityException_WhenClientIdIsInvalid()
    {
        // Arrange
        int clientId = 0;
        decimal value = 100.0m;
        DateTime date = DateTime.Now;
        var client = new ClientEntity(clientId,"Test Client", "email");

        // Act & Assert
        Assert.Throws<ValidationEntityException>(() => new PaymentAggregate(clientId, value, date, client));
    }

    [Fact]
    public void Constructor_ShouldThrowValidationEntityException_WhenValueIsInvalid()
    {
        // Arrange
        int clientId = 1;
        decimal value = 0.0m;
        DateTime date = DateTime.Now;
        var client = new ClientEntity(clientId,"Test Client", "email");

        // Act & Assert
        Assert.Throws<ValidationEntityException>(() => new PaymentAggregate(clientId, value, date, client));
    }

    [Fact]
    public void Constructor_ShouldThrowValidationEntityException_WhenDateIsInvalid()
    {
        // Arrange
        int clientId = 1;
        decimal value = 100.0m;
        DateTime date = DateTime.MinValue;
        var client = new ClientEntity(clientId,"Test Client", "email");


        // Act & Assert
        Assert.Throws<ValidationEntityException>(() => new PaymentAggregate(clientId, value, date, client));
    }

    [Fact]
    public void Constructor_ShouldThrowValidationEntityException_WhenClientIsNull()
    {
        // Arrange
        int clientId = 1;
        decimal value = 100.0m;
        DateTime date = DateTime.Now;
        ClientEntity client = null;

        // Act & Assert
        Assert.Throws<ValidationEntityException>(() => new PaymentAggregate(clientId, value, date, client));
    }
}