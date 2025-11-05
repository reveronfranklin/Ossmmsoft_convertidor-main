public interface IHttpService
{
    Task<GatewayResponse> ExecuteRequestAsync(HttpRequestModel request);
}
