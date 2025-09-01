using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Repositories;
using Template.Infrastructure.Seeders;
using Template.Infrastructure.Services;

namespace Template.Infrastructure.Extensions;
//Server=(localdb)\\mssqllocaldb;Database=OuzonDb;Trusted_Connection=True;
//"Server=ouzondb.mssql.somee.com;Database=ouzondb;User Id=majdlouka_SQLLogin_1;Password=majd2003;TrustServerCertificate=True;MultipleActiveResultSets=True;"
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TemplateDb");
        services.AddDbContext<TemplateDbContext>(options => options.UseSqlServer("Server=ouzondb.mssql.somee.com;Database=ouzondb;User Id=majdlouka_SQLLogin_1;Password=majd2003;TrustServerCertificate=True;MultipleActiveResultSets=True;"));

        //this for identity and jwt when needed
        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>("TemplateTokenProvidor")
            .AddEntityFrameworkStores<TemplateDbContext>()
            .AddDefaultTokenProviders();
        //----------------------------------------------------------------------------------------------------------------
        //services and repositories 
        services.AddScoped<IRolesSeeder, RolesSeeder>();
        services.AddScoped<ICategorySeeder, CategorySeeder>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        // we use typeof because the interface and the class are generic
        // and without it we would have to specify the type(IGenericRepository<Kit>, GenericType<Kit>)
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IKitRepository, KitRepository>();
        services.AddScoped<IImplantRepository, ImplantRepository>();
        services.AddScoped<IToolRepository, ToolRepository>();
        services.AddScoped<IProcedureRepository, ProcedureRepository>();
        services.AddScoped<IHolidayRepository, HolidayRepository>();
        services.AddScoped<IRatingsRepository, RatingRepository>();
        services.AddTransient<INotificationService, NotificationService>();
        services.AddScoped<IFileService, FileService>();

        var fileName = "ouzon-2ce7d-firebase-adminsdk-fbsvc-ec2f64d95b.json";

        // This gets the absolute path to the Infrastructure project's folder at runtime
        var basePath = Directory.GetCurrentDirectory();
        var fullPath = Path.Combine(basePath, fileName);

        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(fullPath)
            });
        }
    }
}
