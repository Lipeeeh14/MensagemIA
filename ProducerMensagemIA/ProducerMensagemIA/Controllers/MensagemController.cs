using Microsoft.AspNetCore.Mvc;
using ProducerMensagemIA.Domain.DTO;
using ProducerMensagemIA.Domain.Services.Interfaces;

namespace ProducerMensagemIA.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MensagemController : ControllerBase
	{
		private readonly IMensagemService _mensagemService;

		public MensagemController(IMensagemService mensagemService)
		{
			_mensagemService = mensagemService;
		}

		[HttpPost]
		public IActionResult SendMessage(MensagemDTO mensagemDTO)
		{
			try
			{
				_mensagemService.SendMessage(mensagemDTO);

				return Accepted("Mensagem enviada com sucesso!");
			}
			catch (Exception ex)
			{
				return BadRequest("Falha ao realizar o envio da mensagem!");
			}
		}
	}
}
