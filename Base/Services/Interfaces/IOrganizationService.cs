using Base.Dtos;
using Microsoft.AspNetCore.Http;

namespace Base.Services.Interfaces;

public interface IOrganizationService
{
    Task Update(OrganizationDto dto, IFormFile? file);
    Task<OrganizationDto> GetInfo();
}