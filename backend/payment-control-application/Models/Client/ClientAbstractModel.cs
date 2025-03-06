namespace payment_control_application.Models.Client
{
    public abstract class ClientAbstractModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}