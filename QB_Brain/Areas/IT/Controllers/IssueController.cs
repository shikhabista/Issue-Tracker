using Base.Dtos.IT.Issue;
using Base.Entities;
using Base.Enums;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels.Issue;
using IT_Web.Extensions;
using IT_Web.Helpers;
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
    private readonly IUserRepo _userRepo;
    private readonly INotificationHelper _notificationHelper;

    public IssueController(ILogger<IssueController> logger, IIssueService issueService, ILabelService labelService, IIssueLabelService issueLabelService, IUserRepo userRepo,
        INotificationHelper notificationHelper)
    {
        _logger = logger;
        _issueService = issueService;
        _labelService = labelService;
        _issueLabelService = issueLabelService;
        _userRepo = userRepo;
        _notificationHelper = notificationHelper;
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
                Repository = repoName,
                RepositoryId = repositoryId
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
        var users = await _userRepo.FindAllAsync();
        var vm = new IssueCreateVm
        {
            LabelList = new SelectList(labels, nameof(Label.Id), nameof(Label.Name)),
            UserList = new SelectList(users, nameof(Base.Entities.User.Id), nameof(Base.Entities.User.Name)),
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
                LabelIds = vm.LabelIds,
                AssigneeId = vm.UserId
            };
            var issue = await _issueService.CreateIssue(issueCreateDto);

            if (vm.LabelIds != null)
            {
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
            }
            _notificationHelper.SetSuccessMsg("Issue created successfully");
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", vm.RepositoryId });
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return View(vm);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var issue = await _issueService.GetIssue(id);
            var labels = await _labelService.GetLabelList();
            var users = await _userRepo.FindAllAsync();
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
                UserList = new SelectList(users, nameof(Base.Entities.User.Id), nameof(Base.Entities.User.Name)),
                UserId = issue.assignee_id
            };
            return View(vm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(IssueEditVm vm)
    {
        try
        {
            var issueEditDto = new IssueDto
            {
                id = vm.Id,
                title = vm.Title,
                description = vm.Description,
                issue_status = (long)IssueStatusEnum.Open,
                date = DateTime.Now,
                repository_id = vm.RepositoryId,
                last_updated = DateTime.Now,
                assignee_id = vm.UserId
            };
            var issue = await _issueService.UpdateIssue(issueEditDto);

            await _issueLabelService.RemoveIssueLabel(vm.Id);
            if (vm.LabelIds != null)
            {
                foreach (var labelId in vm.LabelIds)
                {
                    var dto = new IssueLabel
                    {
                        IssueId = issueEditDto.id,
                        LabelId = labelId,
                        RecDate = DateTime.Now
                    };
                    await _issueLabelService.AddIssueLabel(dto);
                }
            }

            _notificationHelper.SetSuccessMsg("Issue updated successfully");
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", vm.RepositoryId });
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", vm.RepositoryId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> OpenIssue(long id, long repositoryId)
    {
        try
        {
            await _issueService.OpenIssue(id);
            _notificationHelper.SetSuccessMsg("Issue opened");
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", repositoryId });
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", repositoryId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> CloseIssue(long id, long repositoryId)
    {
        try
        {
            await _issueService.CloseIssue(id);
            _notificationHelper.SetSuccessMsg("Issue closed");
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", repositoryId });
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return RedirectToRoute(new { action = "Index", controller = "Issue", area = "IT", repositoryId });

        }
    }
}