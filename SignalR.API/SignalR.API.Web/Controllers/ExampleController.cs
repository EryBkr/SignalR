using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Web.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
