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
        public IActionResult EnviarAlerta([FromBody] Alerta alerta)
        {
            var json = JsonSerializer.Serialize(alerta);
            
            if (alerta.Tipo.ToLower() == "servidor")
            {
                _publisher.Publicar("alerta.servidor", json);
            }
            else if (alerta.Tipo.ToLower() == "rede")
            {
                _publisher.Publicar("alerta.rede", json);
            }
            else if (alerta.Tipo.ToLower() == "banco")
            {
                _publisher.Publicar("alerta.banco", json);
            }
            else if (alerta.Tipo.ToLower() == "segurança")
            {
                _publisher.Publicar("alerta.seguranca", json);
            }
            else
            {
                return BadRequest(new { status = "Tipo de alerta inválido." });
            }

            return Ok(new { status = "Mensagem publicada com sucesso." });
        }
    }
}
