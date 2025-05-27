using Microsoft.AspNetCore.Mvc;
using Alertas.Domain;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/rede/alerta")]
    public class RedeController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido: {alerta.Tipo} - {alerta.Mensagem}");
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}