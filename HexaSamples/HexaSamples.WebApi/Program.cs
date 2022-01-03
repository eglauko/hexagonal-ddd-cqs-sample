using HexaSamples.Domain.OrdemAggregate;
using HexaSamples.Domain.SupportEntities;
using HexaSamples.Infra.Persistence.Configs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adiciona e configura a unidade de trabalho
builder.Services.AddUnitOfWork<SampleDbContext>()
    .ConfigureDbContextPool((sp, options) =>
    {
        options.UseInMemoryDatabase("HexaSamples")
            .UseLazyLoadingProxies();
    })
    .AddRepository<Loja>()
    .AddRepository<Pessoa>()
    .AddRepository<Produto>()
    .AddRepository<OrdemDeVenda>();


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