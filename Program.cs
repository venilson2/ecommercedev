
using Ecommercedev.Source.Application.Services;
using Ecommercedev.Source.Core.Interfaces.Repositories;
using Ecommercedev.Source.Core.Interfaces.Services;
using Ecommercedev.Source.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var connectionString = 
    Environment.GetEnvironmentVariable("MY_CONNECTION_STRING") ?? 
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository>(x =>
    new ClientRepository(connectionString));

builder.Services.AddScoped<CloudinaryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();