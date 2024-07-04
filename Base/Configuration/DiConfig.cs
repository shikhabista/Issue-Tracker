using Base.Helpers;
using Base.Helpers.Interfaces;
using Base.Providers;
using Base.Providers.Interfaces;
using Base.Repo;
using Base.Repo.Interfaces;
using Base.Services;
using Base.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Configuration;

public static class DiConfig
{
    public static IServiceCollection ConfigureBase(this IServiceCollection service)
    {
        service.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return service.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>))
            .AddTransient<IUow, Uow>()
            .AddTransient<IFileHelper, FileHelper>()
            .AddTransient<IContentPathProvider, ContentPathProvider>()
            .AddTransient<IAuthService, AuthService>()
            .AddTransient<IUserRepo, UserRepo>()
            .AddTransient<IBranchRepo, BranchRepo>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IBranchService, BranchService>()
            .AddTransient<IOrganizationRepo, OrganizationRepo>()
            .AddTransient<IOrganizationService, OrganizationService>()
            .AddTransient<IDbService, DbService>()
            .AddTransient<IIssueService, IssueService>()
            .AddTransient<IRepositoryService, RepositoryService>()
            .AddTransient<ILabelService, LabelService>()
            .AddTransient<IIssueLabelService, IssueLabelService>();
    }
}