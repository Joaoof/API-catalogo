using System.Text.Json.Serialization;
using ApiCatalogo.Context;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var seila = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(seila);
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(mySqlConnection, 
    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddTransient<IMyService, MyService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    await next(context);
});

app.MapControllers();

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Middleware final");
});
