namespace payment_control_application.Models;

public abstract class Request
{
    public Pagination Pagination { get; set; } = new Pagination();
}

public class Pagination 
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50; 
}