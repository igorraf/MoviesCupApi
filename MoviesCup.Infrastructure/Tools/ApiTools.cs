using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace MoviesCup.Infrastructure.Tools
{
    public static class ApiTools
    {
        private static readonly HttpClient _client = new HttpClient();

        internal static async Task<List<T>> GetList<T>(string requestUrl)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<T>));
            var streamTask = _client.GetStreamAsync(requestUrl);
            return serializer.ReadObject(await streamTask) as List<T>;
        }
    }
}
