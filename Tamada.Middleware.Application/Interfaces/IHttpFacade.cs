
    public interface IHttpFacade : IDisposable
    {
        Task<HttpResponse<TResponse>> SendRequest<TRequest, TResponse>(HttpRequest<TRequest> httpRequest);
    }

