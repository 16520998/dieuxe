using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dieuxe.Helpers
{
    public class RestClient<T>
    {
        public async Task<T> GetAsync(string url)
        {
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var taskModels = JsonConvert.DeserializeObject<T>(json);
                return taskModels;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetAsync failed:" + e.Message);
                return default(T);
            }
        }
    }
}
