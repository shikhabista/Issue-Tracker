using Base.Entities;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels;
using IT_Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Web.Areas.IT.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class IssueController : Controller
{
    private readonly ILogger<IssueController> _logger;
    private readonly IIssueService _issueService;
    private ILabelService _labelService;

    public IssueController(ILogger<IssueController> logger, IIssueService issueService, ILabelService labelService)
    {
        _logger = logger;
        _issueService = issueService;
        _labelService = labelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var report = await _issueService.GetIssueList();
            return this.SendSuccess("", report);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while fetching data");
            return this.SendError(e.Message);
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> New()
    {
        if (!ModelState.IsValid) return View();
        var labels = await _labelService.GetLabelList();
        var vm = new IssueCreateVm
        {
            LabelList = new SelectList(labels, nameof(Label.Id), nameof(Label.Name))
        };
        return View(vm);
    }
}