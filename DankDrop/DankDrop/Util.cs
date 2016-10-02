using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DankDrop
{
    static class Util
    {
        public const string API_ROOT = "http://172.16.100.247:5000/api";

        public static string GetCombinedBeaconID(string uuid, int major, int minor)
        {
            return $"{uuid}:{major}:{minor}";
        }

        public static async Task<string> HttpGetText(string fullUrl, Encoding encoding)
        {
            var req = WebRequest.CreateHttp(fullUrl);
            using (var resp = (HttpWebResponse)await req.GetResponseAsync())
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    throw new IOException($"Response status code is {resp.StatusCode}");
                }
                using (var stream = resp.GetResponseStream())
                {
                    var reader = new StreamReader(stream, encoding);
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
