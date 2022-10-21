using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace web.Common
{
    public class HttpClientCall
    {
        HttpClient client = new HttpClient();

        // To get the values using http client  
        public HttpResponseMessage GetAsync(string Url)
        {
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync(Url).Result;
        }
        // to insert the values using http client
        public HttpResponseMessage PostAsync(string Url, StringContent Content)
        {
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.PostAsync(Url, Content).Result;
        }
        // to update the values  using http client
        public HttpResponseMessage PutAsync(string Url, StringContent Content)
        {
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.PutAsync(Url, Content).Result;
        }
        // to delete the values using http client
        public HttpResponseMessage DeleteAsync(string Url)
        {
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.DeleteAsync(Url).Result;
        }
    }
}
