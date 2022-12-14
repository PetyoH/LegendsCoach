using Microsoft.AspNetCore.Mvc;

namespace LegendsCoach.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
