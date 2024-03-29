using AccountAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountAPI;

public class DiService
{
    public void ConfigureServices(IServiceCollection services, string connectionString)
    {
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContextPool<ApplicationDbContext>(options => options
            .UseMySql(connectionString, serverVersion)
        );
        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
    
        services.AddControllers();
    }
}