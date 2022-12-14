namespace LegendsCoach.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class Error : Controller
    {
        public IActionResult Error404()
        {
            return this.View();
        }
    }
}
