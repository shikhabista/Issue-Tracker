using Microsoft.AspNetCore.Mvc;

namespace QB_Web.Areas.Admin.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class IssueController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult New()
    {
        return View();
    }
}