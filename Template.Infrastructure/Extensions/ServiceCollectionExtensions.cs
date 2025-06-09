using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Repositories;

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


		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		// we use typeof because the interface and the class are generic
		// and without it we would have to specify the type(IGenericRepository<Kit>, GenericType<Kit>)

		services.AddScoped<IKitRepository, KitRepository>();
	}
}
