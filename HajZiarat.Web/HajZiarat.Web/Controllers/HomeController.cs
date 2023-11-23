using Microsoft.AspNetCore.Mvc;

namespace HajZiarat.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

    }
}
