using Base.Dtos;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QB_Web.Helpers;

namespace QB_Web.Areas.Admin.Controllers;

[Area("Admin")]
public class OrganizationController : Controller
{
    private readonly IOrganizationService _organizationService;
    private readonly INotificationHelper _notificationHelper;

    public OrganizationController(IOrganizationService organizationService, INotificationHelper notificationHelper)
    {
        _organizationService = organizationService;
        _notificationHelper = notificationHelper;
    }

    [HttpGet]
    public async Task<IActionResult> Update()
    {
        var dto = await _organizationService.GetInfo();
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(OrganizationDto dto, IFormFile? file)
    {
        try
        {
            await _organizationService.Update(dto, file);
            _notificationHelper.SetSuccessMsg("Organization Info Updated");
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return View(dto);
        }
    }
}