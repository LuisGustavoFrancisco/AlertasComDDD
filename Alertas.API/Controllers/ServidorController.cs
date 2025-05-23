using Microsoft.AspNetCore.Mvc;
using Alertas.Domain;
using Alertas.Infra.Interfaces;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/servidor")]
    public class ServidorController : ControllerBase
    {
        private readonly IAlertaService _context;

        public ServidorController(IAlertaService context)
        {
            _context = context;
        }

        [HttpPost("alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido: {alerta.Tipo} - {alerta.Mensagem}");
            _context.ProcessarAlertaAsync(alerta);
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}
