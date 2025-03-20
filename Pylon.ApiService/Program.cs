using Microsoft.EntityFrameworkCore;
using Pylon.Application.Interfaces;
using Pylon.Application.Services;
using Pylon.Infrastructure.Persistence;
using Pylon.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Container
builder.Services.AddControllers();

// Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Injection
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserInfoRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => {
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pylon API V1");
		c.RoutePrefix = string.Empty;
	});
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultEndpoints();
app.Run();