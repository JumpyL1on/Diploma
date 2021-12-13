using System.Net.Http;

namespace Frontend.Application.Base
{
    public abstract class BaseHandler
    {
        protected HttpClient HttpClient { get; }

        protected BaseHandler(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}