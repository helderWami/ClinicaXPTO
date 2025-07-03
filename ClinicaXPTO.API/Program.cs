using ClinicaXPTO.API;
using ClinicaXPTO.DAL.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// ============================================================================
// PONTO DE ENTRADA DA APLICAÇÃO - CONFIGURAÇÃO INICIAL
// ============================================================================
// Este arquivo é responsável por configurar e inicializar a aplicação ASP.NET Core
// Define todos os serviços, middlewares e configurações necessárias

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// REGISTO DE SERVIÇOS NO CONTAINER DE DEPENDÊNCIAS
// ============================================================================
// Adiciona os controladores da API (endpoints HTTP)
builder.Services.AddControllers();

// Registra todas as dependências personalizadas da aplicação
// (Repositories, Services, etc.) - definidas em DependencyInjections.cs
builder.Services.AddClinicaXPTODependencies();

// ============================================================================
// CONFIGURAÇÃO DE AUTENTICAÇÃO JWT
// ============================================================================
// Obtém a chave secreta do appsettings.json
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Configura a autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Configuração do Swagger/OpenAPI para documentação da API
// Permite testar os endpoints diretamente no navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============================================================================
// CONFIGURAÇÃO DO BANCO DE DADOS
// ============================================================================
// Registra o contexto do Entity Framework Core
// Configura a conexão com SQL Server usando a string de conexão do appsettings.json
builder.Services.AddDbContext<ClinicaXPTODbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ClinicaXPTOBDConnection")
    ));

// ============================================================================
// CONSTRUÇÃO DA APLICAÇÃO
// ============================================================================
var app = builder.Build();

// ============================================================================
// CONFIGURAÇÃO DO PIPELINE HTTP (MIDDLEWARES)
// ============================================================================
// Em ambiente de desenvolvimento, habilita o Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adiciona o middleware de tratamento de erros simples
app.UseMiddleware<TrataErrosMiddleware>();

// Redireciona HTTP para HTTPS (segurança)
app.UseHttpsRedirection();

// ============================================================================
// MIDDLEWARES DE AUTENTICAÇÃO E AUTORIZAÇÃO
// ============================================================================
// Middleware de autenticação (deve vir antes da autorização)
app.UseAuthentication();

// Middleware de autorização (para endpoints protegidos)
app.UseAuthorization();

// Mapeia os controladores como endpoints da API
app.MapControllers();

// ============================================================================
// INICIALIZAÇÃO DA APLICAÇÃO
// ============================================================================
// Inicia o servidor web e fica à espera de requisições HTTP
app.Run();
