using Microsoft.AspNetCore.Mvc;

namespace MVCECommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Views klasöründeki Home klasörünün içindeki Index.cshtml dosyasını döndürür.
        }
    }
}
