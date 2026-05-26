using Convertidor.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HealthController> _logger;

        public HealthController(IServiceProvider serviceProvider, ILogger<HealthController> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        [HttpGet("databases")]
        public async Task<IActionResult> Databases()
        {
            var checks = new List<DatabaseHealthCheckResult>
            {
                await CheckDatabase<DataContext>("DefaultConnectionRH", "Oracle"),
                await CheckDatabase<DataContextPre>("DefaultConnectionPRE", "Oracle"),
                await CheckDatabase<DataContextRm>("DefaultConnectionRM", "Oracle"),
                await CheckDatabase<DataContextCat>("DefaultConnectionCAT", "Oracle"),
                await CheckDatabase<DataContextSis>("DefaultConnectionSIS", "Oracle"),
                await CheckDatabase<DataContextBm>("DefaultConnectionBM", "Oracle"),
                await CheckDatabase<DataContextBmConteo>("DefaultConnectionBMC", "Oracle"),
                await CheckDatabase<DataContextRhC>("DefaultConnectionRHC", "Oracle"),
                await CheckDatabase<DataContextAdm>("DefaultConnectionADM", "Oracle"),
                await CheckDatabase<DataContextCnt>("DefaultConnectionCNT", "Oracle"),
                await CheckDatabase<DestinoDataContext>("DefaultConnectionPostgres", "PostgreSQL")
            };

            var response = new DatabaseHealthResponse
            {
                Status = checks.All(check => check.IsHealthy) ? "Healthy" : "Unhealthy",
                CheckedAt = DateTimeOffset.UtcNow,
                Databases = checks
            };

            return response.Status == "Healthy" ? Ok(response) : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
        }

        private async Task<DatabaseHealthCheckResult> CheckDatabase<TContext>(string name, string provider)
            where TContext : DbContext
        {
            try
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var context = scope.ServiceProvider.GetRequiredService<TContext>();
                var canConnect = await context.Database.CanConnectAsync();

                return new DatabaseHealthCheckResult
                {
                    Name = name,
                    Context = typeof(TContext).Name,
                    Provider = provider,
                    Status = canConnect ? "Healthy" : "Unhealthy",
                    IsHealthy = canConnect
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validando conexion de base de datos. Name={Name}; Context={Context}; Provider={Provider}", name, typeof(TContext).Name, provider);

                return new DatabaseHealthCheckResult
                {
                    Name = name,
                    Context = typeof(TContext).Name,
                    Provider = provider,
                    Status = "Unhealthy",
                    IsHealthy = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class DatabaseHealthResponse
    {
        public string Status { get; set; }
        public DateTimeOffset CheckedAt { get; set; }
        public List<DatabaseHealthCheckResult> Databases { get; set; }
    }

    public class DatabaseHealthCheckResult
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }
        public bool IsHealthy { get; set; }
        public string Error { get; set; }
    }
}
