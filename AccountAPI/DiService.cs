using AccountAPI.Data;
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
    
        services.AddControllers();
    }
}