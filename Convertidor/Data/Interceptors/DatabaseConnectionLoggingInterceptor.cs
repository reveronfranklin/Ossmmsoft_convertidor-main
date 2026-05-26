using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Convertidor.Data.Interceptors
{
    public class DatabaseConnectionLoggingInterceptor : DbConnectionInterceptor
    {
        private readonly ILogger<DatabaseConnectionLoggingInterceptor> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseConnectionLoggingInterceptor(
            ILogger<DatabaseConnectionLoggingInterceptor> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
        {
            LogConnectionError(connection, eventData);
            base.ConnectionFailed(connection, eventData);
        }

        public override Task ConnectionFailedAsync(
            DbConnection connection,
            ConnectionErrorEventData eventData,
            CancellationToken cancellationToken = default)
        {
            LogConnectionError(connection, eventData);
            return base.ConnectionFailedAsync(connection, eventData, cancellationToken);
        }

        private void LogConnectionError(DbConnection connection, ConnectionErrorEventData eventData)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var requestPath = httpContext?.Request?.Path.Value ?? "Sin request HTTP";
            var requestMethod = httpContext?.Request?.Method ?? "N/A";
            var userName = httpContext?.User?.Identity?.IsAuthenticated == true
                ? httpContext.User.Identity.Name
                : "Anonimo";

            _logger.LogError(
                eventData.Exception,
                "Error de conexion a base de datos. Contexto={DbContext}; Proveedor={Provider}; DataSource={DataSource}; Database={Database}; Metodo={Method}; Ruta={Path}; Usuario={User}",
                eventData.Context?.GetType().Name ?? "No disponible",
                connection.GetType().Name,
                string.IsNullOrWhiteSpace(connection.DataSource) ? "No disponible" : connection.DataSource,
                string.IsNullOrWhiteSpace(connection.Database) ? "No disponible" : connection.Database,
                requestMethod,
                requestPath,
                userName);
        }
    }
}
