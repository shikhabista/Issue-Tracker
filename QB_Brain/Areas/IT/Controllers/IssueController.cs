using Base.Dtos.IT.Issue;
using Base.Entities;
using Base.Enums;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels.Issue;
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
    private readonly IIssueLabelService _issueLabelService;

    public IssueController(ILogger<IssueController> logger, IIssueService issueService, ILabelService labelService, IIssueLabelService issueLabelService)
    {
        _logger = logger;
        _issueService = issueService;
        _labelService = labelService;
        _issueLabelService = issueLabelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(long repositoryId)
    {
        try
        {
            var (report, repoName) = await _issueService.GetIssuesOf(repositoryId);
            var issueReport = new IssueListVm
            {
                IssueList = report,
                Repository = repoName
            };
            return View(issueReport);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while fetching data");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> New(long repositoryId)
    {
        if (!ModelState.IsValid) return View();
        var labels = await _labelService.GetLabelList();
        var vm = new IssueCreateVm
        {
            LabelList = new SelectList(labels, nameof(Label.Id), nameof(Label.Name)),
            RepositoryId = repositoryId
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> New(IssueCreateVm vm)
    {
        try
        {
            var issueCreateDto = new IssueCreateDto
            {
                Title = vm.Title,
                Description = vm.Description,
                IssueStatus = IssueStatusEnum.Open,
                Date = DateTime.Now,
                RepositoryId = vm.RepositoryId,
                LastUpdated = DateTime.Now,
                LabelIds = vm.LabelIds
            };
            var issue = await _issueService.CreateIssue(issueCreateDto);

            foreach (var labelId in vm.LabelIds)
            {
                var dto = new IssueLabel
                {
                    IssueId = issue.id,
                    LabelId = labelId,
                    RecDate = DateTime.Now
                };
                await _issueLabelService.AddIssueLabel(dto);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var issue = await _issueService.GetIssue(id);
            var labels = await _labelService.GetLabelList();
            var labelIds = await _issueLabelService.GetLabelIdsOf(id);
            var vm = new IssueEditVm
            {
                Id = issue.id,
                Title = issue.title,
                Description = issue.description,
                Status = issue.issue_status.ToString(),
                RepositoryId = issue.repository_id,
                Date = issue.date,
                LabelIds = labelIds,
                LabelList = new SelectList(labels, nameof(Label.Id), nameof(Label.Name)),
            };
            return View(vm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> OpenIssue(long id)
    {
        try
        {
            await _issueService.OpenIssue(id);
            return RedirectToAction(nameof(New));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> CloseIssue(long id)
    {
        try
        {
            await _issueService.CloseIssue(id);
            return RedirectToAction(nameof(New));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }
}