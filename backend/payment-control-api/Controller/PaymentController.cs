using payment_control_application.Services.Payment;
using payment_control_application.Models.Payment;
using payment_control_application.Models;
using payment_control_domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace payment_control_api.Controller;
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{clientId}")]
    public async Task<ActionResult<Result<IEnumerable<PaymentResponse>>>> GetByClientId(int clientId)
    {
        var result = await _paymentService.GetByClientId(clientId);
        return StatusCode((int)result.CodReturn, result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<PaymentResponse>>> Post([FromBody] PaymentRequest paymentRequest)
    {
        var result = await _paymentService.Create(paymentRequest);
        return StatusCode((int)result.CodReturn, result);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<Result<PaymentResponse>>> UpdateStatus(int id, [FromQuery] StatusPaymentEnum status )
    {
        var result = await _paymentService.UpdateStatus(id, status);
        return StatusCode((int)result.CodReturn, result);
    }
}
