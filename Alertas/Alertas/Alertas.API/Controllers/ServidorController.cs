using Microsoft.AspNetCore.Mvc;
using Alertas.Domain;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/servidor")]
    public class ServidorController : ControllerBase
    {
        [HttpPost("alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido: {alerta.Tipo} - {alerta.Mensagem}");
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}
