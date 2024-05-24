using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RiadFinalMVC.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
    }
}
