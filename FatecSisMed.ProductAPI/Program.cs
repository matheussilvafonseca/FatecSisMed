using FatecSisMed.MedicoAPI.Context.Entities;
using FatecSisMed.MedicoAPI.Repositories.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using FatecSisMed.MedicoAPI.Services.Entities;
using FatecSisMed.MedicoAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// pegando a string de conexao
var mySqlConnection = builder
    .Configuration.GetConnectionString("DefaultConnection");

// usar para que o Entity Framework
// crie nossas tabelas no banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection))
);

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies());

// adicionando a injeção de dependencia
builder.Services.AddScoped<
    IConvenioRepository, ConvenioRepository>();
builder.Services.AddScoped<
    IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddScoped<
    IMedicoRepository, MedicoRepository>();

builder.Services.AddScoped<
    IConvenioService, ConvenioService>();
builder.Services.AddScoped<
    IEspecialidadeService, EspecialidadeService>();
builder.Services.AddScoped<
    IMedicoService, MedicoService>();

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

