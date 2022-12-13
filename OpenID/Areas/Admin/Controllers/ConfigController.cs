using Microsoft.AspNetCore.Mvc;
using OpenID.Services.Interfaces;
using OpenID.UI.Helpers;
using System.Threading.Tasks;

namespace OpenID.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Config")]
    public class ConfigController : Controller
    {

		private readonly IClientService _clientService;
		public ConfigController(IClientService clientService)
		{
			_clientService = clientService;
		}
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Clients")]
        public async Task<IActionResult> Clients(string id)
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
    }
}
