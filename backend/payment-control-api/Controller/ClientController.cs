using payment_control_application.Services.Client;
using payment_control_application.Models.Client;
using payment_control_application.Models;
using Microsoft.AspNetCore.Mvc;

namespace payment_control_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Create a new client
    /// </summary>
    /// <param name="request"></param>  
    /// <returns>A newly created client</returns>
    /// <response code="201">Returns Created if success</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientCreateRequest request)
    {
        var Result = await _clientService.Create(request);
        return StatusCode((int)Result.CodReturn, Result);
    }

    /// <summary>
    /// Get all clients
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A list of clients</returns>
    /// <response code="200">Returns a list of clients</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="500">If there was an internal server error</response>
    /// <response code="404">If there are no clients</response>
    [HttpGet]
    public async Task<ActionResult<Result<ClientResponse>>> GetClients([FromQuery] ClientRequest request) 
    {
        var result = await _clientService.GetAll(request);
        return StatusCode((int)result.CodReturn, result);
    }


}
