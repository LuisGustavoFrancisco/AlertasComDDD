using Alertas.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/banco/alerta")]
    public class BancoController : ControllerBase
    {
        [HttpPost("Alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido: {alerta.Tipo} - {alerta.Mensagem}");
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}