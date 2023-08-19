 
    public class HttpRequest<TRequest>
    {
        public string? BaseUrl { get; set; }
        public string? Resource { get; set; }
        public Tamada.Middleware.Domain.Entities.Enums.HttpMethod Method { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public TRequest RequestBody { get; set; }
        public int Timeout { get; set; } = 10000;
    }



