using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenID.Dtos.Configuration;
using OpenID.Services.Interfaces;
using OpenID.UI.Helpers;
using System.Threading.Tasks;

namespace OpenID.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Config")]
    public class ConfigController : BaseController
    {

		private readonly IClientService _clientService;
		public ConfigController(IClientService clientService, ILogger<ConfigController> logger) : base(logger)
		{
			_clientService = clientService;
		}
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Client")]
        public async Task<IActionResult> Client(string id)
        {
			if (id.IsNotPresentedValidNumber())
			{
				return NotFound();
			}

			if (id == default)
			{
				var clientDto = _clientService.BuildClientViewModel();
				return View(clientDto);
			}

			int.TryParse(id, out var clientId);
			var client = await _clientService.GetClientAsync(clientId);
			client = _clientService.BuildClientViewModel(client);

			return View(client);
		}
		[Route("Client")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Client(ClientDto client)
		{
			client = _clientService.BuildClientViewModel(client);

			if (!ModelState.IsValid)
			{
				return View(client);
			}

			//Add new client
			if (client.Id == 0)
			{
				var clientId = await _clientService.AddClientAsync(client);
				SuccessNotification(string.Format("SuccessAddClient", client.ClientId),"SuccessTitle");

				return RedirectToAction(nameof(Client), new { Id = clientId });
			}

			//Update client
			await _clientService.UpdateClientAsync(client);
			SuccessNotification(string.Format("SuccessUpdateClient", client.ClientId), "SuccessTitle");

			return RedirectToAction(nameof(Client), new { client.Id });
		}

		[HttpGet]
		[Route("Clients")]
		public async Task<IActionResult> Clients(int? page, string search)
		{
			ViewBag.Search = search;
			return View(await _clientService.GetClientsAsync(search, page ?? 1));
		}
	}
}
