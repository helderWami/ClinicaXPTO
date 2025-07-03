using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class TrataErrosMiddleware
{
    private readonly RequestDelegate _next;
    public TrataErrosMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var resultado = JsonSerializer.Serialize(new { erro = "Algo correu mal.", detalhe = ex.Message });
            await context.Response.WriteAsync(resultado);
        }
    }
} 