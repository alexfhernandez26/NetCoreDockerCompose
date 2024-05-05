using microservices.Context;
using microservices.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("*************************Configrurando los servicios*****************************");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAppDbContext, AppDbContext>();
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer("Server=10.0.0.7,14333;Database=microservicios;User Id=sa;Password=M1st2rPasswOrd!;TrustServerCertificate=true;"));
Console.WriteLine("*************************Finalizado la Configruracion de los servicios*****************************");
var app = builder.Build();

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Creando el migrador manual
var scope = app.Services.CreateScope();
var context = Migrations(scope.ServiceProvider);

//Lando la aplicacion
app.Run();

async Task Migrations(IServiceProvider serviceProvider)
{
    /*Lo que hacemos aqui es recuperar el contexto que tenemos de nuestra DB AppDbContext*/
    var context = serviceProvider.GetService<AppDbContext>();
    var con_appDb = context.Database.GetDbConnection();

    Console.WriteLine($"Conexion actual appDb: {con_appDb.ToString()} {Environment.NewLine} {con_appDb.ConnectionString}");
    Console.WriteLine("*************************Probando Acceso*****************************");

    try
    {
        Console.WriteLine("Base de datos disponible" + context.Database.CanConnect());
        context.Database.Migrate();
    }
    catch (Exception ex)
    {

        Console.WriteLine($"Error al tratar de conectar: {ex.Message}");
    }
}
