namespace payment_control_domain.Exceptions;

public class ValidationEntityException : Exception
{
    public ValidationEntityException()
    {
    }

    public ValidationEntityException(string? message) : base(message)
    {
    }
}