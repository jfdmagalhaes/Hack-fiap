using Domain.Aggregates;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MidiaController : ControllerBase
{
    private readonly IMidiaProducer _midiaProducer;
    private readonly ILogger<MidiaController> _logger;

    public MidiaController(ILogger<MidiaController> logger, IMidiaProducer midiaProducer)
    {
        _logger = logger;
        _midiaProducer = midiaProducer;
    }

    [HttpPost(Name = "Upload")]
    public async Task<IActionResult> Upload([FromForm] Midia midia)
    {
        await _midiaProducer.SendMessageAsync(midia);
        return Ok(); //retornar imgs??
    }
}