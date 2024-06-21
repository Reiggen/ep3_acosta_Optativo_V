using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using ExamenParcial.Validaciones; // Asegúrate de que esto coincida con tu espacio de nombres para los validadores
using Repository.Data; // Asegúrate de que esto coincida con tu espacio de nombres para el acceso al repositorio
using Services.Logica; // Asegúrate de que esto coincida con tu espacio de nombres para el acceso a los servicios

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("postgres");

builder.Services.AddDbContext<Repository.Context.ContextoAplicacionDB>(options =>
{
    options.UseNpgsql(connString);

    // Ensure you have installed EFCore.CheckConstraints package
    options.UseValidationCheckConstraints();
});

// Register the ClienteRepository and ClienteService
builder.Services.AddScoped<ICliente, ClienteRepository>();
builder.Services.AddScoped<ClienteService>();

// Register FluentValidation
// Ensure you have installed FluentValidation.AspNetCore package
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<FacturaValidation>();

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
/*
using Repository.Data; // Add this for repository access
using Services.Logica;
using ExamenParcial.Validaciones;

namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar servicios
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>


            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            var connString = builder.Configuration.GetConnectionString("postgres");

            builder.Services.AddDbContext<Repository.Context.ContextoAplicacionDB>(options =>
            {
                options.UseNpgsql(connString);
                options.UseValidationCheckConstraints();
            });

            // Register the ClienteRepository and ClienteService
            builder.Services.AddScoped<ICliente, ClienteRepository>();
            builder.Services.AddScoped<ClienteService>();
            // Register FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidation>();
            builder.Services.AddValidatorsFromAssemblyContaining<FacturaValidation>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}*/
