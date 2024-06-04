using Base.Constants;
using Base.Dtos;
using Base.Entities;
using Base.Helpers.Interfaces;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Base.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepo _organizationRepo;
    private readonly IUow _uow;
    private readonly IFileHelper _fileHelper;

    public OrganizationService(IOrganizationRepo organizationRepo, IUow uow, IFileHelper fileHelper)
    {
        _organizationRepo = organizationRepo;
        _uow = uow;
        _fileHelper = fileHelper;
    }

    public async Task Update(OrganizationDto dto, IFormFile? file)
    {
        var infos = await _organizationRepo.FindAllAsync();
        if (file != null)
        {
            var fileName = await _fileHelper.UploadFile(file, Organization.FileDir);
            await SetOrgValue(infos, OrganizationKeyConst.Logo, fileName);
        }
        var props = dto.GetType().GetProperties();
        foreach (var item in props)
        {
            var itemValue = item.GetValue(dto, null)?.ToString();
            if(!string.IsNullOrEmpty(itemValue))await SetOrgValue(infos, item.Name, itemValue);
        }

        await _uow.CommitAsync();
    }

    public async Task<OrganizationDto> GetInfo()
    {
        var infos = await _organizationRepo.FindAllAsync();
        var logoName = infos.FirstOrDefault(x => x.ItemKey == OrganizationKeyConst.Logo)?.ItemValue ?? string.Empty;
        return new OrganizationDto()
        {
            Name = infos.FirstOrDefault(x => x.ItemKey == OrganizationKeyConst.Name)?.ItemValue ?? string.Empty,
            Address = infos.FirstOrDefault(x => x.ItemKey == OrganizationKeyConst.Address)?.ItemValue ?? string.Empty,
            PhoneNo = infos.FirstOrDefault(x => x.ItemKey == OrganizationKeyConst.PhoneNo)?.ItemValue ?? string.Empty,
            Email = infos.FirstOrDefault(x => x.ItemKey == OrganizationKeyConst.Email)?.ItemValue ?? string.Empty,
            Website = infos.FirstOrDefault(x => x.ItemKey == OrganizationKeyConst.Website)?.ItemValue ?? string.Empty,
            Logo = $"{DirRouteConstant.Attachments}/{Organization.FileDir}/{logoName}"
        };
    }

    private async Task SetOrgValue(List<Organization> infos, string itemKey, string? itemValue)
    {
        var info = infos.FirstOrDefault(x => x.ItemKey.Equals(itemKey));
        if (info == null)
        {
            info = new Organization(itemKey, itemValue);
            await _uow.CreateAsync(info);
        }
        else
        {
            info.ItemValue = itemValue;
            _uow.Update(info);
        }
    }
}