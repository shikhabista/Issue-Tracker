using Microsoft.AspNetCore.Mvc;

namespace QB_Web.Areas.IT.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class RepositoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult New()
    {
        return View();
    }
}