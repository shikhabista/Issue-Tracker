using Base.Entities;
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

    public RepositoryController(ILogger<RepositoryController> logger, IRepositoryService repositoryService)
    {
        _logger = logger;
        _repositoryService = repositoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> New(RepositoryCreateVm model)
    {
        try
        {
            var repo = new Repository
            {
                Name = model.Name,
                Description = model.Description,
                Visibility = model.Visibility
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