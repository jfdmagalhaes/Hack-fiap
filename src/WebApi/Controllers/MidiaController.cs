using Domain.Aggregates;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MidiaController : ControllerBase
{
    private readonly IMidiaProducer _midiaProducer;
    private readonly IMidiaService _service;

    public MidiaController(IMidiaProducer midiaProducer, IMidiaService service)
    {
        _midiaProducer = midiaProducer ?? throw new ArgumentNullException(nameof(midiaProducer));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadAsync([FromForm] MidiaModel midia)
    {
        try
        {
            await _service.SaveMidiaAndSplit(midia);

            //Envio para Rabbit e armazenamento no sql
            var midiaEntity = new Midia
            {
                FilePath = midia.FormFile.FileName,
                CreationDate = DateTime.Now
            };

            await _midiaProducer.SendMessageAsync(midiaEntity);

            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync()
    {
        var midias = await _service.GetAllMidias();
        if (midias.Any() is false) return NotFound($"Não foi encontrada nenhuma midia");

        return Ok(midias);
    }
}