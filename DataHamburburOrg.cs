using System.IO;
using System.Net.Http;
using Valve.Newtonsoft.Json.Linq;

namespace Console
{
    public static class DataHamburburOrg
    {
        private static JObject DataBackingField;

        public static JObject Data
        {
            get
            {
                if (DataBackingField != null)
                    return DataBackingField;

                using HttpClient    httpClient   = new HttpClient();
                HttpResponseMessage dataResponse = httpClient.GetAsync("https://hamburbur.org/data").Result;
                using Stream        dataStream   = dataResponse.Content.ReadAsStreamAsync().Result;
                using StreamReader  dataReader   = new StreamReader(dataStream);
                string              json         = dataReader.ReadToEnd().Trim();
                DataBackingField = JObject.Parse(json);

                return DataBackingField;
            }
        }

        public static void ResetDataBackingField() => DataBackingField = null;
    }
}