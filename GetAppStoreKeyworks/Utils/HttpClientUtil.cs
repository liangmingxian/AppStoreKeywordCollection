using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetAppStoreKeyworks.Utils
{
    class HttpClientUtil
    {

        static HttpClientUtil instance = null;
        private HttpClient _httpClient;

        private HttpClientUtil()
        {
            this._httpClient = new HttpClient();

            this._httpClient.DefaultRequestHeaders.Add("X-Apple-Store-Front", "143465,26");
            this._httpClient.DefaultRequestHeaders.Add("User-Agent", "AppStore/2.0 iOS/8.3");
            this._httpClient.DefaultRequestHeaders.Add("'X-Apple-Tz", "-18000");
            this._httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            this._httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-us, en;q=0.50");
        }

        public static HttpClientUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpClientUtil();
                }

                return instance;
            }
        }

        public void SetStoreFront(string storeFronId)
        {
            this._httpClient.DefaultRequestHeaders.Remove("X-Apple-Store-Front");
            this._httpClient.DefaultRequestHeaders.Add("X-Apple-Store-Front", storeFronId + ",26");
        }

        public string GetStoreFront()
        {
            string s = "";
            foreach (string value in this._httpClient.DefaultRequestHeaders.GetValues("X-Apple-Store-Front"))
            {
                s = value;
            }

            return s;
        }

        public string GetMethod(string url)
        {
            Task<HttpResponseMessage> task = this._httpClient.GetAsync(new Uri(url));
            HttpResponseMessage response = task.Result;
            string rs = response.Content.ReadAsStringAsync().Result;
            return rs;
        }

        static void Main()
        {
            //string rs = HttpClientUtil.Instance.GetMethod("https://search.itunes.apple.com/WebObjects/MZSearchHints.woa/wa/trends?maxCount=20");
            //Console.WriteLine(rs);
            HttpClientUtil h = HttpClientUtil.Instance;
            string rs = h.GetMethod("https://search.itunes.apple.com/WebObjects/MZSearchHints.woa/wa/trends?maxCount=20");
            Console.WriteLine(rs);
            Console.WriteLine("======================================================");
            h.SetStoreFront("143441");
            rs = h.GetMethod("https://search.itunes.apple.com/WebObjects/MZSearchHints.woa/wa/trends?maxCount=20");
            Console.WriteLine(rs);
        }
    }
}
