using Base.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QB_Web.Areas.v1.Admin.Requests;
using QB_Web.Extensions;

namespace QB_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[AllowAnonymous]
public class LoginController : Controller
{
    private readonly IAuthService _authService;

    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginReq());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginReq req)
    {
        try
        {
            await _authService.Login(req.Username, req.Password);
            return RedirectToAction(nameof(Index), nameof(Index), new {Area = "Admin"});
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }
}