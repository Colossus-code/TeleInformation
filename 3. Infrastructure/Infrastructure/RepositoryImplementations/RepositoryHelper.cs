using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementations
{
    /// <summary>
    /// Works to make diferent kind of calls to obtain information
    /// </summary>
    public static class RepositoryHelper
    {
        private static HttpClient httpClient = new HttpClient();

        #region ApiCalls
        public static async Task<List<T>> GetList<T>(string rootPath)
        {

            HttpResponseMessage get = await httpClient.GetAsync(rootPath);

            string responseAsString = await get.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseAsString)) return new List<T>();

            return JsonConvert.DeserializeObject<List<T>>(responseAsString);
        }
        public static async Task<T> GetObject<T>(string rootPath, T defaultValue)
        {

            HttpResponseMessage get = await httpClient.GetAsync(rootPath);

            string responseAsString = await get.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseAsString)) return defaultValue;

            return JsonConvert.DeserializeObject<T>(responseAsString);
        }
        #endregion
        #region LocalFileCall
        public static List<T> GetListLocal<T>(string rootPath)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(rootPath));
        }
        #endregion

        // In case of we want to get another type of calls we can factor here and call in the class we want.
   
    }
}
