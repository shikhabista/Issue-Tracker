using Base.Entities;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.ViewModels.Repository;
using IT_Web.Areas.IT.VIewModels.Repository;
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
            var list = await _repositoryService.GetData();

            return View(list);
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
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating repository");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var repo = await _repositoryService.GetRepository(id);
            var repoEditVm = new RepositoryEditVm
            {
                Id = id,
                Name = repo.Name,
                Description = repo.Description,
                Visibility = repo.Visibility,
                Branch = repo.Branch,
                IsPublic = repo.Visibility == Repository.Public,
                IsPrivate = repo.Visibility == Repository.Private,
            };
            return View(repoEditVm);
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating repository");
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RepositoryEditVm vm)
    {
        try
        {
            var repo = new Repository
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                Visibility = vm.IsPrivate != null ? Repository.Private : Repository.Public,
                Status = StatusEnum.Active,
                Branch = vm.Branch
            };
            await _repositoryService.UpdateRepository(repo);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating repository");
            return this.SendError(e.Message);
        }
    }
}