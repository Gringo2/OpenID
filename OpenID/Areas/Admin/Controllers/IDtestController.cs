using Microsoft.AspNetCore.Mvc;

namespace OpenID.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/IDtest")]
    public class IDtestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
