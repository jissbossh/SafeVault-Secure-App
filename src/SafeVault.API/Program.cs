using SafeVault.API.Endpoints;
using SafeVault.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SecurityService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapUserEndpoints();

app.Run();
