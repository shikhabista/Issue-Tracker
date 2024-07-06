using Base.Entities;
using Base.Services;
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
    private readonly IIssueLabelService _issueLabelService;

    public LabelController(ILogger<LabelController> logger, ILabelService labelService, IIssueLabelService issueLabelService)
    {
        _logger = logger;
        _labelService = labelService;
        _issueLabelService = issueLabelService;
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
            var isDuplicate = await _labelService.CheckIfDuplicateName(vm.Code.ToLower().Trim());
            if (isDuplicate) throw new Exception($"Label Code {vm.Code} already exists.");
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
            var issueLabel = await _issueLabelService.CheckIfLabelInUse(id);
            if (issueLabel) throw new Exception("Can't delete label. Label is assigned to an issue");
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