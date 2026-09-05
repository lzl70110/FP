using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Infrastructure.Data;
using FP.Infrastructure.Repositories;
using FP.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using FP.Application.Contracts.Identity;
using FP.Domain.Entities.Positions;
using FP.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped(
    typeof(IRepository<>),
    typeof(Repository<>));

builder.Services.AddScoped(
    typeof(ICrudService<>),
    typeof(CrudService<>));

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<IEntityIdentity<Position>, PositionIdentity>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddSingleton<DateTimeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();