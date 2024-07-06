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
using IT_Web.Helpers;
using UserPasswordUpdateVm = IT_Web.Areas.Admin.Requests.UserPasswordUpdateVm;

namespace IT_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRepo _userRepo;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly INotificationHelper _notificationHelper;

    public UserController(IUserService userService, IUserRepo userRepo, ICurrentUserProvider currentUserProvider, INotificationHelper notificationHelper)
    {
        _userService = userService;
        _userRepo = userRepo;
        _currentUserProvider = currentUserProvider;
        _notificationHelper = notificationHelper;
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
        var data = await _userRepo.GetQueryable().Where(x => x.UserName.ToLower() != "admin@gmail.com").Select(x => new UserVm()
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
        try
        {
            var n = req.Name.Split();
            bool isNumber = false;
            foreach (var nu in n)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (nu == $"{i}")
                    {
                        isNumber = true;
                    }  
                }
            }
            if(isNumber) throw new Exception("Number is not allowed");
            if (!req.Password.Equals(confirmpass)) throw new Exception("Confirm password did not match");
            var branchId = await _currentUserProvider.GetUserBranchId();
            var userDto = new UserAddDto()
            {
                Name = req.Name,
                Address = req.Address,
                ContactNo = req.ContactNo,
                Email = req.Email,
                UserName = req.UserName,
                Password = req.Password,
                BranchId = branchId
            };
            await _userService.Create(userDto);
            _notificationHelper.SetSuccessMsg("User created successfully");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
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
    public async Task<IActionResult> Update(long id, UserUpdateReq req)
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
            await _userService.Update(user, userUpdateDto);
            _notificationHelper.SetSuccessMsg("User updated successfully");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return View(req);
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
            await _userService.UpdatePassword(user, vm.OldPassword, vm.NewPassword);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _notificationHelper.SetErrorMsg(e.Message);
            return View(req);
        }
    }
}