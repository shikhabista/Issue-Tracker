using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels;
using IT_Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IT_Web.Areas.IT.Controllers;

[Area("IT")]
[Route("[area]/[controller]/[action]")]
public class LabelController : Controller
{
    private readonly ILogger<LabelController> _logger;
    private readonly ILabelService _labelService;

    public LabelController(ILogger<LabelController> logger, ILabelService labelService)
    {
        _logger = logger;
        _labelService = labelService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> New()
    {
        try
        {
            // var labels = await _labelService.GetLabelList();
            // LabelCreateVm vm = new LabelCreateVm { Labels = labels };
            return View();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }
}