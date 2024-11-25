using Microsoft.EntityFrameworkCore;
using RestauranteApi.Context;
using RestauranteApi.Services.Implementaciones;
using RestauranteApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
