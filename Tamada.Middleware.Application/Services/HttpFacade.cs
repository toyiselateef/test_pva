using Newtonsoft.Json;
using RestSharp;
using System.Net; 
 

    public class HttpFacade : IHttpFacade
    {
        public async Task<HttpResponse<TResponse>> SendRequest<TRequest, TResponse>(HttpRequest<TRequest> httpRequest)
        {
            var client = CreateRestClient(httpRequest.BaseUrl?? throw new BadRequestException("No BaseUrl"));
            var request = CreateRestRequest(httpRequest);
            var response = await client.ExecuteAsync(request);

            var httpResponse = CreateHttpResponse<TResponse>(response);

            HandleResponse(httpResponse);

            if (httpResponse.IsSuccessful)
            {
                httpResponse.Data = JsonConvert.DeserializeObject<TResponse>(response.Content);
            }

            return httpResponse;
        }

        private RestClient CreateRestClient(string baseUrl)
        {
            return new RestClient(baseUrl);
        }

        private RestRequest CreateRestRequest<TRequest>(HttpRequest<TRequest> httpRequest)
        {
            var request = new RestRequest(httpRequest.Resource, ConvertToRestSharpMethod(httpRequest.Method))
            {
                Timeout = httpRequest.Timeout
            };

            AddHeaders(request, httpRequest.Headers);
            AddParameters(request, httpRequest.Parameters);
            AddRequestBody(request, httpRequest.RequestBody);

            return request;
        }

        private Method ConvertToRestSharpMethod(Tamada.Middleware.Domain.Entities.Enums.HttpMethod httpMethod)
        {
            return httpMethod switch
            {
                Tamada.Middleware.Domain.Entities.Enums.HttpMethod.Get => Method.Get,
                Tamada.Middleware.Domain.Entities.Enums.HttpMethod.Post => Method.Post,
                Tamada.Middleware.Domain.Entities.Enums.    HttpMethod.Put => Method.Put,
                Tamada.Middleware.Domain.Entities.Enums.HttpMethod.Delete => Method.Delete,
                _ => throw new ArgumentException($"Unsupported HTTP method: {httpMethod}")
            };
        }

        private void AddHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
        }

        private void AddParameters(RestRequest request, Dictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }
        }

        private void AddRequestBody(RestRequest request, object requestBody)
        {
            if (requestBody != null)
            {
                request.AddJsonBody(requestBody);
            }
        }

        private HttpResponse<TResponse> CreateHttpResponse<TResponse>(RestResponse response)
        {
            //var headers = response.Headers?.ToDictionary(header => header.Name, header => header.Value.ToString());
               
            //var headers = response.Headers?.Distinct().ToDictionary(header => header.Name, header => header.Value.ToString());
            return new HttpResponse<TResponse>
            {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessful,
                Content = response.Content
                //Headers = headers
            };
        }

        private void HandleResponse<TResponse>(HttpResponse<TResponse> response)
        {
            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException($"HTTP request failed with status code: {response.StatusCode}");
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnAuthorizedException($"HTTP request failed with status code: {response.StatusCode}");
                }
                else
                {
                    throw new ServerErrorException($"HTTP request failed with status code: {response.StatusCode}");
                }
            }
        }

        public void Dispose()
        {
            // Nothing specific to dispose in this case.
        }
    }
   