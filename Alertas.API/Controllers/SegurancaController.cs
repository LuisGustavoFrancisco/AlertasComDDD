using Microsoft.AspNetCore.Mvc;
using Alertas.Domain;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/seguranca/alerta")]
    public class SegurancaController : ControllerBase
    {
        [HttpPost("Alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido: {alerta.Tipo} - {alerta.Mensagem}");
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}