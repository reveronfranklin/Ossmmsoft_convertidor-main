
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
using Swashbuckle;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

//Repository SIS

builder.Services.AddTransient<ISisUsuarioRepository, SisUsuarioRepository>();
//Services Sis
builder.Services.AddTransient<ISisUsuarioServices, SisUsuarioServices>();



//Services Presupuesto
builder.Services.AddTransient<IPRE_PRESUPUESTOSService, PRE_PRESUPUESTOSService>();
builder.Services.AddTransient<IPRE_V_SALDOSServices, PRE_V_SALDOSServices>();
builder.Services.AddTransient<IPRE_V_DENOMINACION_PUCServices, PRE_V_DENOMINACION_PUCServices>();




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





// Register AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description ="Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In=ParameterLocation.Header,
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey
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



var destinoConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionPostgres");
builder.Services.AddDbContext<DestinoDataContext>(options =>
      options.UseNpgsql(destinoConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

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

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<DestinoDataContext>();
//    db.Database.Migrate();
//}


//dotnet ef migrations add InitialCreate --context DestinoDataContext --output-dir Migrations/DestinoDataContext

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");
app.UseAuthentication();
 app.UseAuthorization();

app.MapControllers();

app.Run();
