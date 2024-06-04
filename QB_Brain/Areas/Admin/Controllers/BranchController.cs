using Base.Dtos;
using Base.Repo.Interfaces;
using Base.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QB_Web.Extensions;

namespace QB_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
public class BranchController : Controller
{
    private readonly IBranchService _branchService;
    private readonly IBranchRepo _branchRepo;

    public BranchController(IBranchService branchService, IBranchRepo branchRepo)
    {
        _branchService = branchService;
        _branchRepo = branchRepo;
    }

    public async Task<IActionResult> Report()
    {
        var list = await _branchRepo.GetQueryable().Select(x => x.ToMiniInfo).ToListAsync();
        return this.SendSuccess("", list);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BranchDto dto)
    {
        try
        {
            var branch = await _branchService.Create(dto);
            return this.SendSuccess("", branch.ToMiniInfo);
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(long id, [FromBody] BranchDto dto)
    {
        try
        {
            var branch = await _branchRepo.FindOrThrowAsync(id);
            await _branchService.Update(branch, dto);
            return this.SendSuccess("", branch.ToMiniInfo);
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Activate(long id)
    {
        try
        {
            var branch = await _branchRepo.FindOrThrowAsync(id);
            await _branchService.Activate(branch);
            return this.SendSuccess("", branch.ToMiniInfo);
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Deactivate(long id)
    {
        try
        {
            var branch = await _branchRepo.FindOrThrowAsync(id);
            await _branchService.Deactivate(branch);
            return this.SendSuccess("", branch.ToMiniInfo);
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetOptions()
    {
        var list = await _branchRepo.GetQueryable().Select(x => x.ToMiniInfo).ToListAsync();
        return this.SendSuccess("", list);
    }
}