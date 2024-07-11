using Microsoft.AspNetCore.Mvc;

namespace IT_Web.Areas.Admin.Controllers;

[Area("Admin")]
public class IndexController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}