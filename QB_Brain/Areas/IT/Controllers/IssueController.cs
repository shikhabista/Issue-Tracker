using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels;
using IT_Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IT_Web.Areas.IT.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class IssueController : Controller
{
    private readonly ILogger<IssueController> _logger;
    private IIssueService _issueService;

    public IssueController(ILogger<IssueController> logger, IIssueService issueService)
    {
        _logger = logger;
        _issueService = issueService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // try
        // {
        //     // var report = await _issueService.GetIssueList();
        //     return this.SendSuccess("");
        // }
        // catch (Exception e)
        // {
        //     _logger.LogError("Error while fetching data");
        //     return this.SendError(e.Message);
        // }
        return View();
    }

    [HttpGet]
    public IActionResult New()
    {
        return View();
    }
}