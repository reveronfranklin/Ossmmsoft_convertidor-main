
using Convertidor.Data;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Data.Repository.Sis;
using Convertidor.Services;
using Convertidor.Services.Catastro;
using Convertidor.Services.Presupuesto;
using Convertidor.Services.Sis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using Convertidor.Data.Interfaces.Bm;
using Swashbuckle;
using Swashbuckle.AspNetCore.Filters;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Repository.Rh;
using Convertidor.Services.Rh;
using Microsoft.AspNetCore.HttpOverrides;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();



builder.Services.AddTransient<IRH_HISTORICO_NOMINARepository, RH_HISTORICO_NOMINARepository>();
builder.Services.AddTransient<IHistoricoNominaRepository, HistoricoNominaRepository>();
builder.Services.AddTransient<IRH_HISTORICO_PERSONAL_CARGORepository, RH_HISTORICO_PERSONAL_CARGORepository>();
builder.Services.AddTransient<IHistoricoPersonalCargoRepository, HistoricoPersonalCargoRepository>();

builder.Services.AddTransient<IIndiceCategoriaProgramaRepository, IndiceCategoriaProgramaRepository>();
builder.Services.AddTransient<IConceptosRetencionesRepository, ConceptosRetencionesRepository>();
builder.Services.AddTransient<IHistoricoRetencionesRepository, HistoricoRetencionesRepository>();

//Repository Presupuesto
builder.Services.AddTransient<IPRE_PRESUPUESTOSRepository, PRE_PRESUPUESTOSRepository>();
builder.Services.AddTransient<IPRE_INDICE_CAT_PRGRepository, PRE_INDICE_CAT_PRGRepository>();
builder.Services.AddTransient<IPRE_V_SALDOSRepository, PRE_V_SALDOSRepository>();
builder.Services.AddTransient<IPRE_V_DENOMINACION_PUCRepository, PRE_V_DENOMINACION_PUCRepository>();
builder.Services.AddTransient<IPRE_V_DOC_COMPROMISOSRepository, PRE_V_DOC_COMPROMISOSRepository>();
builder.Services.AddTransient<IPRE_V_DOC_CAUSADORepository, PRE_V_DOC_CAUSADORepository>();
builder.Services.AddTransient<IPRE_V_DOC_PAGADORepository, PRE_V_DOC_PAGADORepository>();
builder.Services.AddTransient<IPRE_V_DOC_BLOQUEADORepository, PRE_V_DOC_BLOQUEADORepository>();
builder.Services.AddTransient<IPRE_V_DOC_MODIFICADORepository, PRE_V_DOC_MODIFICADORepository>();
builder.Services.AddTransient<IPRE_ASIGNACIONESRepository, PRE_ASIGNACIONESRepository>();



//Services Presupuesto
builder.Services.AddTransient<IPRE_PRESUPUESTOSService, PRE_PRESUPUESTOSService>();
builder.Services.AddTransient<IPRE_V_SALDOSServices, PRE_V_SALDOSServices>();
builder.Services.AddTransient<IPRE_V_DENOMINACION_PUCServices, PRE_V_DENOMINACION_PUCServices>();
builder.Services.AddTransient<IPRE_V_MTR_DENOMINACION_PUCRepository, PRE_V_MTR_DENOMINACION_PUCRepository>();
builder.Services.AddTransient<IPRE_V_MTR_DENOMINACION_PUCService, PRE_V_MTR_DENOMINACION_PUCService>();
builder.Services.AddTransient<IPRE_V_MTR_UNIDAD_EJECUTORARepository, PRE_V_MTR_UNIDAD_EJECUTORARepository>();
builder.Services.AddTransient<IPRE_V_MTR_UNIDAD_EJECUTORAService, PRE_V_MTR_UNIDAD_EJECUTORAService>();
builder.Services.AddTransient<IPRE_V_DOC_COMPROMISOSServices, PRE_V_DOC_COMPROMISOSServices>();
builder.Services.AddTransient<IPRE_V_DOC_CAUSADOServices, PRE_V_DOC_CAUSADOServices>();
builder.Services.AddTransient<IPRE_V_DOC_PAGADOServices, PRE_V_DOC_PAGADOServices>();
builder.Services.AddTransient<IPRE_V_DOC_BLOQUEADOServices, PRE_V_DOC_BLOQUEADOServices>();
builder.Services.AddTransient<IPRE_V_DOC_MODIFICADOServices, PRE_V_DOC_MODIFICADOServices>();
builder.Services.AddTransient<IPRE_PLAN_UNICO_CUENTASRepository, PRE_PLAN_UNICO_CUENTASRepository>();
builder.Services.AddTransient<IPrePlanUnicoCuentasService, PrePlanUnicoCuentasService>();

builder.Services.AddTransient<IPRE_RELACION_CARGOSRepository, PRE_RELACION_CARGOSRepository>();
builder.Services.AddTransient<IPreDescriptivaRepository, PreDescriptivaRepository>();
builder.Services.AddTransient<IPreDescriptivasService, PreDescriptivasService>();
builder.Services.AddTransient<IPreTitulosRepository, PreTitulosRepository>();
builder.Services.AddTransient<IPreTituloService, PreTituloService>();
builder.Services.AddTransient<IPreCargosRepository, PreCargosRepository>();
builder.Services.AddTransient<IPreCargosService, PreCargosService>();
builder.Services.AddTransient<IPRE_RELACION_CARGOSRepository, PRE_RELACION_CARGOSRepository>();
builder.Services.AddTransient<IPreRelacionCargosService, PreRelacionCargosService>();



//Repository SIS

builder.Services.AddTransient<ISisUsuarioRepository, SisUsuarioRepository>();
builder.Services.AddTransient<IOssConfigRepository, OssConfigRepository>();
builder.Services.AddTransient<ISisUbicacionNacionalRepository, SisUbicacionNacionalRepository>();
builder.Services.AddTransient<ISisUbicacionService, SisUbicacionService>();



//Services Sis
builder.Services.AddTransient<ISisUsuarioServices, SisUsuarioServices>();
builder.Services.AddTransient<IOssConfigServices, OssServices>();
builder.Services.AddTransient<IEmailServices, EmailServices>();






//CATASTRO
builder.Services.AddTransient<ICAT_FICHARepository, CAT_FICHARepository>();

builder.Services.AddTransient<ICAT_FICHAService, CAT_FICHAService>();


//RH
builder.Services.AddTransient<IHistoricoNominaService, HistoricoNominaService>();
builder.Services.AddTransient<IHistoricoPersonalCargoService, HistoricoPersonalCargoService>();
builder.Services.AddTransient<IIndiceCategoriaProgramaService, IndiceCategoriaProgramaService>();
builder.Services.AddTransient<IConceptosRetencionesService, ConceptosRetencionesService>();
builder.Services.AddTransient<IHistoricoRetencionesService, HistoricoRetencionesService>();
builder.Services.AddHttpClient<PetroClientService>();
builder.Services.AddTransient<IPetroClientService, PetroClientService>();

builder.Services.AddTransient<IRhPeriodoService, RhPeriodoService>();
builder.Services.AddTransient<IRhHistoricoMovimientoService, RhHistoricoMovimientoService>();
builder.Services.AddTransient<IRhEducacionRepository, RhEducacionRepository>();
builder.Services.AddTransient<IRhEducacionService, RhEducacionService>();



builder.Services.AddTransient<IRhTipoNominaRepository, RhTipoNominaRepository>();
builder.Services.AddTransient<IRhPeriodoRepository, RhPeriodoRepository>();
builder.Services.AddTransient<IRhPersonasRepository, RhPersonasRepository>();
builder.Services.AddTransient<IRhPersonaService, RhPersonaService>();
builder.Services.AddTransient<IRhHistoricoMovimientoRepository, RhHistoricoMovimientoRepository>();

builder.Services.AddTransient<IRhTipoNominaService, RhTipoNominaService>();
builder.Services.AddTransient<IRhDescriptivaRepository, RhDescriptivaRepository>();
builder.Services.AddTransient<IRhDescriptivasService, RhDescriptivasService>();

builder.Services.AddTransient<IRhDireccionesRepository, RhDireccionesRepository>();
builder.Services.AddTransient<IRhDireccionesService, RhDireccionesService>();

builder.Services.AddTransient<IRhConceptosRepository, RhConceptosRepository>();
builder.Services.AddTransient<IRhConceptosService, RhConceptosService>();

builder.Services.AddTransient<IRhProcesoRepository, RhProcesoRepository>();
builder.Services.AddTransient<IRhProcesosService, RhProcesosService>();
builder.Services.AddTransient<IRhProcesoDetalleRepository, RhProcesoDetalleRepository>();
builder.Services.AddTransient<IRhRelacionCargosRepository, RhRelacionCargosRepository>();
builder.Services.AddTransient<IRhRelacionCargosService, RhRelacionCargosService>();

builder.Services.AddTransient<IRhMovNominaRepository, RhMovNominaRepository>();
builder.Services.AddTransient<IRhComunicacionessRepository, RhComunicacionessRepository>();
builder.Services.AddTransient<IRhAdministrativosRepository, RhAdministrativosRepository>();
builder.Services.AddTransient<IRhAdministrativosService, RhAdministrativosService>();


//BM Repository
builder.Services.AddTransient<IBM_V_BM1Repository, BM_V_BM1Repository>();
//BM Services
builder.Services.AddTransient<IBM_V_BM1Service, BM_V_BM1Service>();



// Register AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>(); 
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionRH");
builder.Services.AddDbContext<DataContext>(options =>
      options.UseOracle(connectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var preConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionPRE");
builder.Services.AddDbContext<DataContextPre>(options =>
      options.UseOracle(preConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));



var rmConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionRM");
builder.Services.AddDbContext<DataContextRm>(options =>
      options.UseOracle(rmConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var catConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionCAT");
builder.Services.AddDbContext<DataContextCat>(options =>
      options.UseOracle(catConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var sisConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionSIS");
builder.Services.AddDbContext<DataContextSis>(options =>
      options.UseOracle(sisConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var bmConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionBM");
builder.Services.AddDbContext<DataContextBm>(options =>
    options.UseOracle(bmConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));



var destinoConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionPostgres");
builder.Services.AddDbContext<DestinoDataContext>(options =>
      options.UseNpgsql(destinoConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

/*var redisConnectionString = builder.Configuration.GetConnectionString("redisConnection");
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(redisConnectionString));*/

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value )),
            ValidateIssuer=false,
            ValidateAudience=false
        };

    });

var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<DestinoDataContext>();
//    db.Database.Migrate();
//}


//dotnet ef migrations add InitialCreate --context DestinoDataContext --output-dir Migrations/DestinoDataContext

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
//Server de desarrollo
//app.Urls.Add("http://216.244.81.116:5000");
app.Urls.Add("http://localhost:5000");
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corspolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
