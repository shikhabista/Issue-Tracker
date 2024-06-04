using System.Transactions;
using Base.Dtos;
using Base.Entities;
using Base.Repo.Interfaces;

namespace Base.Services;

public interface IBranchService
{
    Task<Branch> Create(BranchDto dto);
    Task Update(Branch branch, BranchDto dto);
    Task Activate(Branch branch);
    Task Deactivate(Branch branch);
}

public class BranchService : IBranchService
{
    private readonly IUow _uow;

    public BranchService(IUow uow)
    {
        _uow = uow;
    }

    public async Task<Branch> Create(BranchDto dto)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var branch = new Branch()
        {
            Name = dto.Name,
            Code = dto.Code,
            Address = dto.Address,
            ContactNo = dto.ContactNo,
            Email = dto.Email
        };
        await _uow.CreateAsync(branch);
        await _uow.CommitAsync();
        scope.Complete();
        return branch;
    }

    public async Task Update(Branch branch, BranchDto dto)
    {
        branch.Name = dto.Name;
        branch.Code = dto.Code;
        branch.Address = dto.Address;
        branch.ContactNo = dto.ContactNo;
        branch.Email = dto.Email;
        _uow.Update(branch);
        await _uow.CommitAsync();
    }

    public async Task Activate(Branch branch)
    {
        if (branch.Status == StatusEnum.Active)
            throw new Exception("Branch is already active");
        branch.Status = StatusEnum.Active;
        _uow.Update(branch);
        await _uow.CommitAsync();
    }
    
    public async Task Deactivate(Branch branch)
    {
        if (branch.Status == StatusEnum.Inactive)
            throw new Exception("Branch is already inactive");
        branch.Status = StatusEnum.Inactive;
        _uow.Update(branch);
        await _uow.CommitAsync();
    }
}