using Base.Entities;
using Base.Extensions;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels;
using IT_Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IT_Web.Areas.IT.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class RepositoryController : Controller
{
    private readonly ILogger<RepositoryController> _logger;
    private readonly IRepositoryService _repositoryService;

    public RepositoryController(ILogger<RepositoryController> logger, IRepositoryService repositoryService)
    {
        _logger = logger;
        _repositoryService = repositoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {

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
                Status = StatusEnum.Active
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