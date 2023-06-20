using BackEndAPI.Modelos;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using BackEndAPI.Services.Contrato;
using BackEndAPI.Services.Implementación;
using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Utilidades;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Server = localhost\\SQLEXPRESS; DataBase = master; Trusted_Connection = True; TrustServerCertificate = true

builder.Services.AddDbContext<MasterContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
 
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region PETICIONES DE API REST
app.MapGet("/persona/lista", async (
    IPersonaService _PersonaServicio,
    IMapper _mapper
    ) =>
{
    var listapersonas = await _PersonaServicio.GetList();
    var listapersonasDTO = _mapper.Map<List<PersonaDTO>>(listapersonas);

    if (listapersonasDTO.Count > 0)
        return Results.Ok(listapersonasDTO);
    else
        return Results.NotFound();
});

app.MapGet("/empleado/lista", async (
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
{
    var listaempleado = await _empleadoServicio.GetList();
    var listaempleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaempleado);

    if (listaempleadoDTO.Count > 0)
        return Results.Ok(listaempleadoDTO);
    else
        return Results.NotFound();
});

app.MapPost("/empleado/guardar", async (
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) => {
        var _empleado = _mapper.Map<Empleado>(modelo);
        var _empleadoCreado = await _empleadoServicio.Add(_empleado);
        if (_empleadoCreado.EmpleadoId != 0)
            return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
});


app.MapPut("/empleado/actualizar/{empleadoId}", async (
    int empleadoId,
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoServicio,
    IMapper _mapper) => {
        var _encontrado = await _empleadoServicio.Get(empleadoId);
        if(_encontrado is null)return Results.NotFound();

        var _empleado =_mapper.Map<Empleado>(modelo);
        _encontrado.Cargo = _empleado.Cargo;
        _encontrado.PersonaId = _empleado.PersonaId;
        _encontrado.FechaContrato = _empleado.FechaContrato;

        var respuesta = await _empleadoServicio.Update(_encontrado);
        if (respuesta)
            return Results.Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });


app.MapDelete("/empleado/eliminar/{empleadoId}", async (
    int empleadoId,
     IEmpleadoService _empleadoServicio
     ) => {
         var _encontrado = await _empleadoServicio.Get(empleadoId);
         if (_encontrado is null) return Results.NotFound();

         var respuesta = await _empleadoServicio.Delete(_encontrado);
         if(respuesta)
             return Results.Ok();
         else
             return Results.StatusCode(StatusCodes.Status500InternalServerError);
     });
#endregion  


app.UseCors("NuevaPolitica");

app.Run();

