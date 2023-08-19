using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

 


    public class HttpResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string? Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public T Data { get; set; }
    }


