using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pylon.Infrastructure.Persistence;

var builder = DistributedApplication.CreateBuilder(args);

// Add the DbContext from Pylon.Infrastructure
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.Pylon_ApiService>("apiservice");

builder.AddProject<Projects.Pylon_Web>("webfrontend")
	.WithExternalHttpEndpoints()
	.WithReference(cache)
	.WaitFor(cache)
	.WithReference(apiService)
	.WaitFor(apiService);

builder.Build().Run();
