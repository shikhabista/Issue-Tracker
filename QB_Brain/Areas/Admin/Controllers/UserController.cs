using Base.Dtos;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QB_Web.Areas.v1.Admin.Requests;
using QB_Web.Extensions;

namespace QB_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRepo _userRepo;

    public UserController(IUserService userService, IUserRepo userRepo)
    {
        _userService = userService;
        _userRepo = userRepo;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> RegisterInitial()
    {
        try
        {
            await _userService.RegisterAdminUser();
            return this.SendSuccess("Admin user register successfully");
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepo.GetQueryable().Include(x => x.Branch).ToListAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]UserAddDto dto)
    {
        try
        {
            var user = await _userService.Create(dto);
            return this.SendSuccess("User registered successfully", new { user.Id, user.Name });
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(long id, [FromBody] UserUpdateDto dto)
    {
        try
        {
            var user = await _userRepo.FindOrThrowAsync(id);
            await _userService.Update(user, dto);
            return this.SendSuccess("User updated successfully");
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] UserPasswordUpdateVm vm)
    {
        try
        {
            var user = await _userRepo.FindOrThrowAsync(vm.Id);
            await _userService.UpdatePassword(user, vm.OldPassword, vm.NewPassword);
            return this.SendSuccess("Password update successfully");
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }
}