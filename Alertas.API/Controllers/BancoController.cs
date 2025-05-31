using Alertas.Domain;
using Alertas.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/banco")]
    public class BancoController : ControllerBase
    {
        private readonly IAlertaRepository _alertasSubscriber;

        public BancoController(IAlertaRepository alertasSubscriber)
        {
            _alertasSubscriber = alertasSubscriber;
        }

        [HttpPost("Alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido de fila_banco: {alerta.Tipo} - {alerta.Mensagem}");
            _alertasSubscriber.Salvar(alerta);
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}