using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Repositories;
using Template.Infrastructure.Seeders;

namespace Template.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("TemplateDb");
		services.AddDbContext<TemplateDbContext>(options => options.UseSqlServer(connectionString));

		//this for identity and jwt when needed
		services.AddIdentityCore<User>()
			.AddRoles<IdentityRole>()
			.AddTokenProvider<DataProtectorTokenProvider<User>>("TemplateTokenProvidor")
			.AddEntityFrameworkStores<TemplateDbContext>()
			.AddDefaultTokenProviders();
        //----------------------------------------------------------------------------------------------------------------
        //services and repositories 
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
		services.AddScoped<IRolesSeeder, RolesSeeder>();
    }
}
