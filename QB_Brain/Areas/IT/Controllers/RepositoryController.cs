using Base.Dtos;
using Base.Entities;
using Base.Enums;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels;
using IT_Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IT_Web.Areas.IT.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class RepositoryController : Controller
{
    private readonly ILogger<RepositoryController> _logger;
    private readonly IRepositoryService _repositoryService;
    private readonly IIssueService _issueService;

    public RepositoryController(ILogger<RepositoryController> logger, IRepositoryService repositoryService, IIssueService issueService)
    {
        _logger = logger;
        _repositoryService = repositoryService;
        _issueService = issueService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var list = await _repositoryService.GetRepositoryList();
            var totalIssueCount = 0;
            var totalOpenIssue = 0;
            var totalClosedIssue = 0;
            RepositoryDto repositoryDto;
            foreach (var repo in list)
            {
                var repoIssues = await _issueService.GetIssuesOf(repo.Id);
                repositoryDto = new RepositoryDto
                {
                    Id = repo.Id,
                    Name = repo.Name,
                    Description = repo.Description,
                    Visibility = repo.Visibility,
                    Branch = repo.Branch,
                    TotalIssuesCount = repoIssues.Count,
                    TotalOpenIssuesCount = repoIssues.Count(a => a.issue_status == (long)IssueStatusEnum.Open),
                    TotalClosedIssuesCount = repoIssues.Count(a => a.issue_status == (long)IssueStatusEnum.Close)
                };
            }
            
            
            
            var vm = new RepositoryReportVm
            {
                RepositoryList = list,
                TotalOpenIssuesCount = 0,
                TotalClosedIssuesCount = 0,
                TotalIssuesCount = 0
            };
            return View(vm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }

        return View();
    }

    [HttpGet]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> New(RepositoryCreateVm model)
    {
        try
        {
            var visibility = model.IsPrivate != null ? Repository.Private : Repository.Public;
            var repo = new Repository
            {
                Name = model.Name,
                Description = model.Description,
                Visibility = visibility,
                RecDate = DateTime.Now,
                Status = StatusEnum.Active,
                Branch = "Master"
            };
            await _repositoryService.CreateRepository(repo);
            return this.SendSuccess("");
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating repository");
            return this.SendError(e.Message);
        }
    }
}