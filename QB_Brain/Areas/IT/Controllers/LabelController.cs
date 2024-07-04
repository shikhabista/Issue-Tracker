using Base.Entities;
using Base.Services.Interfaces;
using IT_Web.Areas.IT.VIewModels.Label;
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
    public async Task<IActionResult> New()
    {
        try
        {
            var labels = await _labelService.GetLabelList();
            LabelCreateVm vm = new LabelCreateVm { Labels = labels };
            return View(vm);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> New(LabelCreateVm vm)
    {
        try
        {
            var label = new Label
            {
                Name = vm.Name,
                Description = vm.Description,
                Code = vm.Code,
                RecDate = DateTime.Now
            };
            await _labelService.CreateLabel(label);
            return RedirectToAction(nameof(New));
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
            var label = await _labelService.GetLabel(id);
            var vm = new LabelEditVm
            {
                Id = label.Id,
                Name = label.Name,
                Description = label.Description,
                Code = label.Code
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
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _labelService.DeleteLabel(id);
            return RedirectToAction(nameof(New));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            return this.SendError(e.Message);
        }
    }
}