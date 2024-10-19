using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// servcies container

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// options.UseSqlite(config.GetConnectionString("DefaultConnection"));
// var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

string connString = "";

// if (builder.Environment.IsDevelopment()) 
connString = builder.Configuration.GetConnectionString("DefaultConnection");
// else 
// {
// // Use connection string provided at runtime by FlyIO.
//         var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

//         // Parse connection URL to connection string for Npgsql
//         connUrl = connUrl.Replace("postgres://", string.Empty);
//         var pgUserPass = connUrl.Split("@")[0];
//         var pgHostPortDb = connUrl.Split("@")[1];
//         var pgHostPort = pgHostPortDb.Split("/")[0];
//         var pgDb = pgHostPortDb.Split("/")[1];
//         var pgUser = pgUserPass.Split(":")[0];
//         var pgPass = pgUserPass.Split(":")[1];
//         var pgHost = pgHostPort.Split(":")[0];
//         var pgPort = pgHostPort.Split(":")[1];
// 	var updatedHost = pgHost.Replace("flycast", "internal");

//         connString = $"Server={updatedHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
// }
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
// var serverVersion = ServerVersion.AutoDetect(connString);
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseMySql(connString, serverVersion);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fixer", Version = "v1" });
});

// middleware

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x.AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // Allow any origin
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fixer v1");
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");



using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try 
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManager, context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
