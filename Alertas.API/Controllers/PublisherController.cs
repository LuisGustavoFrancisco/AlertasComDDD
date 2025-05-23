using System.Text.Json;
using Alertas.Domain;
using Alertas.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alertas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IAlertaPublisher _publisher;

        public PublisherController(IAlertaPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost("alerta")]
        public IActionResult EnviarAlerta([FromBody] object alerta)
        {
            var json = JsonSerializer.Serialize(alerta);
            _publisher.Publicar("alerta.servidor", json);
            

            return Ok(new { status = "Mensagem publicada com sucesso." });
        }
    }
}
