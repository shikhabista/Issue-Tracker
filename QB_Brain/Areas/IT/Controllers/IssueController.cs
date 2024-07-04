using Base.Entities;
using Base.Enums;
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
    private readonly ILabelService _labelService;

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
            return View(report);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while fetching data");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> New(long id)
    {
        if (!ModelState.IsValid) return View();
        var labels = await _labelService.GetLabelList();
        var vm = new IssueCreateVm
        {
            LabelList = new SelectList(labels, nameof(Label.LabelId), nameof(Label.Name)),
            RepositoryId = id
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> New(IssueCreateVm vm)
    {
        try
        {
            var issue = new Issue
            {
                Title = vm.Title,
                Description = vm.Description,
                IssueStatus = IssueStatusEnum.Open,
                Date = DateTime.Now,
                RepositoryId = vm.RepositoryId,
                LastUpdated = DateTime.Now
            };
            await _issueService.CreateIssue(issue);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }
}