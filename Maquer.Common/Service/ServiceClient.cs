using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Maquer.Common.Service
{
    public class ServiceClient
    {
        public static async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }

        public static async Task<HttpResponseMessage> PostAsync(string url,object data)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return await client.PostAsync(url, content);
            }
        }
    }
}
