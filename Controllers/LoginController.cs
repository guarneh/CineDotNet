using CIneDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIneDotNet.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyContext _context;
        public LoginController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
