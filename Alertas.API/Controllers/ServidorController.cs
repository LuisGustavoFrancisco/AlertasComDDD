using Alertas.Domain;
using Alertas.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alertas.API.Controllers
{
    [ApiController]
    [Route("api/servidor")]
    public class ServidorController : ControllerBase
    {
        private readonly IAlertaSubscriber _alertasSubscriber;

        public ServidorController(IAlertaSubscriber alertasSubscriber)
        {
            _alertasSubscriber = alertasSubscriber;
        }

        [HttpPost("alerta")]
        public IActionResult ReceberAlerta([FromBody] Alerta alerta)
        {
            Console.WriteLine($"Alerta recebido de fila_servidor: {alerta.Tipo} - {alerta.Mensagem}");
            _alertasSubscriber.Salvar(alerta);
            return Ok(new { status = "ok", mensagem = alerta.Mensagem });
        }
    }
}
