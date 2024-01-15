using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Infra.CrossCutting;
using API_MessageTrigger.Infra.Data.Repository;
using API_MessageTrigger.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IBaseRepository<MessageTrigger>, BaseRepository<MessageTrigger>>();
builder.Services.AddScoped<IBaseService<MessageTrigger>, BaseService<MessageTrigger>>();
builder.Services.AddScoped<IRequestEvolutionApi, RequestEvolutionApi>();
builder.Services.AddScoped<IServiceMessageTrigger, ServiceMessageTrigger>();
builder.Services.AddHttpClient();
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
