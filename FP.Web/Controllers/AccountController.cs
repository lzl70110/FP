 
using Microsoft.AspNetCore.Mvc;

namespace FP.Web.Controllers;

public class AccountController : Controller
{
    // Временен action само за преглед на Login изгледа.
    // Автентикацията ще бъде реализирана по-късно с ASP.NET Core Identity.
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
}
 
