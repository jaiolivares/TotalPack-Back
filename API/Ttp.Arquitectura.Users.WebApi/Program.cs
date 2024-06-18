using Microsoft.EntityFrameworkCore;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Queries;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Repository;
using Ttp.Arquitectura.Users.WebApi.Models.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("users-db") ?? throw new InvalidOperationException("Connection string 'users-db' not encontrada.")));

builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<AddUserHandler>();
builder.Services.AddScoped<GetUsersHandler>();

IConfigurationSection appSettingsSection = builder.Configuration.GetSection("AppSettings");
AppSettings appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(appSettingsSection);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();