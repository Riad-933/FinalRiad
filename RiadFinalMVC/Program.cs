using Business.Services.Abstracts;
using Business.Services.Concrets;
using Core.Models;
using Core.RepositoryConcrets;
using Data.DAL;
using Data.RepositoryConcrets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;


    var builder = WebApplication.CreateBuilder(args);

    
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("default"))

    );

    builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;

        options.User.RequireUniqueEmail = false;

    }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IServiceService, ServiceService>();
    
    builder.Services.AddScoped<IServiceRepository, ServiceRepository>();


    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");

        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
         );
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();




