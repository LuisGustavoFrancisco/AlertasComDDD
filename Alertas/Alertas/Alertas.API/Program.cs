using Alertas.Infra;
using Alertas.Infra.Interfaces;
using Alertas.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SqlContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddSingleton<IAlertaPublisher, AlertaPublisher>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
