using LogicaAccesoDatos.EF.Repositorios;
using LogicaAplicacion.CasosDeUso.Auditoria;
using LogicaAplicacion.CasosDeUso.Equipo;
using LogicaAplicacion.CasosDeUso.ObjetoCeleste;
using LogicaAplicacion.CasosDeUso.Observacion;
using LogicaAplicacion.CasosDeUso.Prestamo;
using LogicaAplicacion.CasosDeUso.Usuario;
using LogicaAplicacion.InterfacesCasosDeUso.Auditoria;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaAplicacion.InterfacesCasosDeUso.ObjetoCeleste;
using LogicaAplicacion.InterfacesCasosDeUso.Observacion;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("MvcPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5186",
            "http://obligatoriocliente.somee.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddDbContext<LogicaAccesoDatos.EF.ObligatorioContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("ObligatorioDB")
           ));

// Repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioEquipo, RepositorioEquipo>();
builder.Services.AddScoped<IRepositorioPrestamo, RepositorioPrestamo>();
builder.Services.AddScoped<IRepositorioObservacion, RepositorioObservacion>();
builder.Services.AddScoped<IRepositorioObjetoCeleste, RepositorioObjetoCeleste>();
builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();

// CU usuario
builder.Services.AddScoped<IAltaUsuario, AltaUsuarioCU>();
builder.Services.AddScoped<IObtenerUsuarios, ObtenerUsuariosCU>();
builder.Services.AddScoped<IObtenerSocios, ObtenerSociosCU>();
builder.Services.AddScoped<IObtenerCoordinadores, ObtenerCoordinadoresCU>();
builder.Services.AddScoped<ILogin, LoginCU>();
builder.Services.AddScoped<IObtenerUsuarioXId, ObtenerUsuarioXIdCU>();

// CU equipo
builder.Services.AddScoped<IAltaEquipo, AltaEquipoCU>();
builder.Services.AddScoped<IDeleteEquipo, DeleteEquipoCU>();
builder.Services.AddScoped<IObtenerEquipos, ObtenerEquiposCU>();
builder.Services.AddScoped<IObtenerEquipoXId, ObtenerEquipoXIdCU>();
builder.Services.AddScoped<IObtenerEquipos, ObtenerEquiposCU>();
builder.Services.AddScoped<IObtenerTelescopios, ObtenerTelescopiosCU>();
builder.Services.AddScoped<IUpdate, UpdateCU>();

//CU prestamo
builder.Services.AddScoped<IAltaPrestamo, AltaPrestamoCU>();
builder.Services.AddScoped<IDevolucionPrestamo, DevolucionPrestamoCU>();
builder.Services.AddScoped<IObtenerPrestamos, ObtenerPrestamosCU>();
builder.Services.AddScoped<IObtenerPrestamoXId, ObtenerPrestamoXIdCU>();
builder.Services.AddScoped<IPrestamosActivosDeSocio, PrestamosActivosDeSocioCU>();
builder.Services.AddScoped<IObtenerPrestamosEntreFechas, ObtenerPrestamosEntreFechasCU>();
builder.Services.AddScoped<IObtenerSociosXTelescopio, ObtenerSociosXTelescopioCU>();
builder.Services.AddScoped<IObtenerPrestamosXCoordinador, ObtenerPrestamosXCoordinadorCU>();
builder.Services.AddScoped<IPrestamosVigentesDeSocio, PrestamosVigentesDeSocioCU>();

//CU observacion
builder.Services.AddScoped<IAltaObservacion, AltaObservacionCU>();
builder.Services.AddScoped<IConsultaIA, ConsultaIA>();
builder.Services.AddScoped<IEvaluarObservacion, EvaluarObservacionCU>();

//CU objetoscelestes
builder.Services.AddScoped<IObtenerObjetosCelestes, ObtenerObjetosCelestesCU>();
builder.Services.AddScoped<IListadoRankingObjetos, ListadoRankingObjetosCU>();

//CU auditorias
builder.Services.AddScoped<IObtenerAuditoriasXPrestamo, ObtenerAuditoriasXPrestamoCU>();

//Configuracion Autenticacion con token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones =>
{
    opciones.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretTokenKey").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// Configurar la autorización
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("MvcPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
