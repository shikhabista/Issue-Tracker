using Base.Services.Interfaces;
using IT_Web.Areas.Admin.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IT_Web.Extensions;

namespace IT_Web.Areas.Admin.Controllers;

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
            return RedirectToRoute(new { action = "Index", controller = "Repository", area = "IT" });
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }
    
    public async Task<IActionResult> LogOut()
    {
        await _authService.LogOut();
        return RedirectToAction(nameof(Index));
    }
}