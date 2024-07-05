using Base.Dtos;
using Base.Providers.Interfaces;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;
using IT_Web.Areas.Admin.Requests;
using IT_Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT_Web.Extensions;
using UserPasswordUpdateVm = IT_Web.Areas.Admin.Requests.UserPasswordUpdateVm;

namespace IT_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRepo _userRepo;
    private readonly ICurrentUserProvider _currentUserProvider;

    public UserController(IUserService userService, IUserRepo userRepo, ICurrentUserProvider currentUserProvider)
    {
        _userService = userService;
        _userRepo = userRepo;
        _currentUserProvider = currentUserProvider;
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
        var data = await _userRepo.GetQueryable().Where(x=>x.UserName.ToLower() != "admin@gmail.com").Select(x => new UserVm()
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address ?? string.Empty,
            ContactNo = x.ContactNo,
            Email = x.Email,
            UserName = x.UserName
        }).ToListAsync();
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(UserAddRequest req)
    {
        var confirmpass = req.ConfirmPassword;
        if (req.Password.Equals(confirmpass))
        {
            try
            {
                var BranchId = await _currentUserProvider.GetUserBranchId();
                var userDto = new UserAddDto()
                {
                    Name = req.Name,
                    Address = req.Address,
                    ContactNo = req.ContactNo,
                    Email = req.Email,
                    UserName = req.UserName,
                    Password = req.Password,
                    BranchId = BranchId
                };
                await _userService.Create(userDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return this.SendError(e.Message);
                
            }

        }
        else
        {
            return this.SendError("Confirm password doesn't matched");
            return View(req);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var data = await _userRepo.GetQueryable().Where(x => x.Id == id).SingleOrDefaultAsync();
        var Updatereq = new UserUpdateReq()
        {
            Id = data.Id,
            Name = data.Name,
            Address = data.Address,
            Email = data.Email,
            ContactNo = data.ContactNo
        };
        return View(Updatereq);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(long id,UserUpdateReq req)
    {
        try
        {
            var userUpdateDto = new UserUpdateDto()
            {
                Name = req.Name,
                Address = req.Address,
                Email = req.Email,
                ContactNo = req.ContactNo
            };
            var user = await _userRepo.FindOrThrowAsync(id);
            await _userService.Update(user,userUpdateDto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
        public async Task<IActionResult> UpdatePassword(long id)
        {
            var data = await _userRepo.GetQueryable().Where(x => x.Id == id).SingleOrDefaultAsync();
            var ResetPass = new PasswordUpdateReq()
            {
                Id = data.Id
            };
            return View(ResetPass);
        }
    
    [HttpPost]
    public async Task<IActionResult> UpdatePassword(long id, PasswordUpdateReq req)
    {
        try
        {
            var vm = new UserPasswordUpdateVm()
            {
                Id = req.Id,
                OldPassword = req.OldPassword,
                NewPassword = req.NewPassword
            };
            
            
            var user = await _userRepo.FindOrThrowAsync(vm.Id);
            await _userService.UpdatePassword(user,vm.OldPassword,vm.NewPassword);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }
}