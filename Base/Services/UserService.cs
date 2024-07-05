using System.Transactions;
using Base.Constants;
using Base.Dtos;
using Base.Entities;
using Base.Helpers;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;

namespace Base.Services;

public class UserService : IUserService
{
    private readonly IUow _uow;
    private readonly IUserRepo _userRepo;
    private readonly IBranchRepo _branchRepo;

    public UserService(IUow uow, IUserRepo userRepo, IBranchRepo branchRepo)
    {
        _uow = uow;
        _userRepo = userRepo;
        _branchRepo = branchRepo;
    }

    public async Task RegisterAdminUser()
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        if (await _userRepo.CheckIfExistAsync(x => x.Id == (long)IdConstants.MainUserId))
            throw new Exception("Admin user already registered");
        var branch = new Branch()
        {
            Id = (long)IdConstants.MainBranchId,
            Name = "Main Branch",
            Address = "Birtamode",
            Code = 100,
            ContactNo = "980000000",
            Status = StatusEnum.Active
        };
        await _uow.CreateAsync(branch);
        var user = new User()
        {
            Id = (long)IdConstants.MainUserId,
            Name = "Admin",
            UserName = "qbmin@gmail.com",
            Email = "qbmin@gmail.com",
            NormalizedEmail = "QBMIN@GMAIL.COM",
            NormalizedUserName = "QBMIN@GMAIL.COM",
            ContactNo = "980000000",
            BranchId = (long)IdConstants.MainBranchId,
            PasswordHash = Crypter.Encrypt("Qb_P#32_I404"),
            SecurityStamp = ""
        };
        await _uow.CreateAsync(user);
        await _uow.CommitAsync();
        scope.Complete();
    }

    public async Task<User> Create(UserAddDto dto)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        ValidatePassword(dto.Password);
        var user = new User
        {
            Name = dto.Name,
            UserName = dto.UserName,
            NormalizedUserName = dto.UserName.ToLower(),
            Email = dto.Email,
            NormalizedEmail = dto.Email.ToUpper(),
            ContactNo = dto.ContactNo,
            Address = dto.Address,
            Branch = await _branchRepo.FindOrThrowAsync(dto.BranchId),
            PasswordHash = Crypter.Encrypt(dto.Password),
            SecurityStamp = ""
        };
        await _uow.CreateAsync(user);
        await _uow.CommitAsync();
        scope.Complete();
        return user;
    }

    public async Task Update(User user, UserUpdateDto dto)
    {
        user.Name = dto.Name;
        user.Address = dto.Address;
        user.ContactNo = dto.ContactNo;
        user.Email = dto.Email;
        user.NormalizedEmail = dto.Email.ToUpper();
        _uow.Update(user);
        await _uow.CommitAsync();
    }

    public async Task UpdatePassword(User user, string oldPass, string newPass)
    {
        if (!Crypter.IsMatching(oldPass, user.PasswordHash))
            throw new Exception("Failed to match old password");
        ValidatePassword(newPass);
        user.PasswordHash = Crypter.Encrypt(newPass);
        _uow.Update(user);
        await _uow.CommitAsync();
    }

    private void ValidatePassword(string password)
    {
        if (password.Length < 5) throw new Exception("Password must be at least 5 character long");
        if (!password.Any(char.IsDigit)) throw new Exception("Password should contain at least one number");
    }
}