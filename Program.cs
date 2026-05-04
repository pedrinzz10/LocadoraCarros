using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Data;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Configuração dos serviços
// ---------------------------------------------------------------

// Adiciona controllers ao container de DI
builder.Services.AddControllers();

// Configura o DbContext com Oracle usando a connection string do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configura o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title   = "Locadora de Carros API",
        Version = "v1",
        Description = "API REST para cadastro de carros e cálculo de locações."
    });
});

var app = builder.Build();

// ---------------------------------------------------------------
// Configuração do pipeline HTTP
// ---------------------------------------------------------------

// Habilita o Swagger em todos os ambientes
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora de Carros API v1");
    options.RoutePrefix = string.Empty; // Swagger abre na raiz: http://localhost:PORT/
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
