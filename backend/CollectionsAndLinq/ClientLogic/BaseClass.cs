﻿using Newtonsoft.Json;
using System.Text;

namespace CollectionsAndLinq.ClientLogic
{
    internal abstract class BaseClass
    {
        protected HttpClient _httpClient;

        public BaseClass()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7105/api/DataProcessing/");
        }

        protected static T Deserializer<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        protected static StringContent Serializer<T>(T entity)
        {
            var json = JsonConvert.SerializeObject(entity);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
