using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Services.MaterialServices;
using EducationPortal.DataAccess.DataContext;
using EducationPortal.DataAccess.Repositories;
using EducationPortal.Presentation;
using Microsoft.EntityFrameworkCore;
using Task1.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("Default")
        ),
        ServiceLifetime.Transient
    );

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IArticleMaterialService, ArticleMaterialService>();
//builder.Services.AddCustomServices();
builder.Services.AddAutoMapper(typeof(EducationPortal.BusinessLogic.MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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