using AccountAPI;
using AccountAPI.Data;
using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("AccountDb"));


var connectionString = builder.Configuration.GetConnectionString("AccountDB") ?? string.Empty;
if (connectionString is null)
{
    throw new InvalidOperationException("SQL Connection String cannot be empty!");
}

new DiService().ConfigureServices(services: builder.Services, connectionString);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger().RequireAuthorization("Admin");
}

app.UseHttpsRedirection();
app.MapIdentityApi<IdentityUser>();
app.UseAuthorization();

app.MapControllers();

app.Run();
