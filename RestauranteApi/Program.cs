using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Services.Implementaciones;
using RestauranteApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<RestauranteContext>(
    options  => options.UseMySql(conString,ServerVersion.AutoDetect(conString))
);
builder.Services.AddScoped<ICategoriaService,CategoriaService>();
builder.Services.AddScoped<IClienteService,ClienteService>();
builder.Services.AddScoped<IPuestoService,PuestoService>();
builder.Services.AddScoped<IEmpleadoService,EmpleadoService>();
builder.Services.AddScoped<IReservaService,ReservaService>();
builder.Services.AddScoped<IPlatilloService,PlatilloService>();
builder.Services.AddScoped<IPedidoService,PedidoService>();
builder.Services.AddScoped<IVotoService,VotoService>();
builder.Services.AddControllers();
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
