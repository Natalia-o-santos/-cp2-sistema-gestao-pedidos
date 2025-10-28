using Microsoft.AspNetCore.Mvc;
using MottuDelivery.Application.DTOs;

namespace MottuDelivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Verifica a saúde da API
    /// </summary>
    /// <returns>Status da API</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    public IActionResult Get()
    {
        return Ok(new { 
            Status = "Healthy", 
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Service = "Mottu Delivery API"
        });
    }
}
