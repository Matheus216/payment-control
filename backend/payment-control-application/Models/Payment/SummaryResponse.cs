namespace payment_control_application.Models.Payment;

public class SummaryResponse
{
    public int TotalPending { get; set; }
    public int TotalPaid { get; set; }
    public int TotalCanceled { get; set; }
    public int TotalClients { get; set; }
}
