using payment_control_domain.Entities;
using payment_control_domain.Exceptions;

namespace payment_control_domain.Tests.Entities;

public class ClientEntityTests
{
    [Fact]
    public void Constructor_ValidNameAndEmail_CreatesClient()
    {
        // Arrange
        string validName = "John Doe";
        string validEmail = "john.doe@example.com";

        // Act
        var client = new ClientEntity(validName, validEmail);

        // Assert
        Assert.Equal(validName, client.Name);
        Assert.Equal(validEmail, client.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_InvalidName_ThrowsValidationEntityException(string invalidName)
    {
        // Arrange
        string validEmail = "john.doe@example.com";

        // Act & Assert
        var exception = Assert.Throws<ValidationEntityException>(() => new ClientEntity(invalidName, validEmail));
        Assert.Equal("Nome inválido", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-email")]
    [InlineData("john.doe@com")]
    public void Constructor_InvalidEmail_ThrowsValidationEntityException(string invalidEmail)
    {
        // Arrange
        string validName = "John Doe";

        // Act & Assert
        var exception = Assert.Throws<ValidationEntityException>(() => new ClientEntity(validName, invalidEmail));
        Assert.Equal("Email inválido", exception.Message);
    }

    [Fact]
    public void Constructor_WithId_ValidNameAndEmail_CreatesClient()
    {
        // Arrange
        int id = 1;
        string validName = "John Doe";
        string validEmail = "john.doe@example.com";

        // Act
        var client = new ClientEntity(id, validName, validEmail);

        // Assert
        Assert.Equal(id, client.Id);
        Assert.Equal(validName, client.Name);
        Assert.Equal(validEmail, client.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_WithId_InvalidName_ThrowsValidationEntityException(string invalidName)
    {
        int id = 1;
        string validEmail = "john.doe@example.com";

        var exception = Assert.Throws<ValidationEntityException>(() => new ClientEntity(id, invalidName, validEmail));
        Assert.Equal("Nome inválido", exception.Message);
    }
}

