using susalem.wpf.Models.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Services.IServices
{
    public interface IDataProvider
    {
        public interface IDataProvider
        {
            string Url { get; set; }
            TimeSpan TimeOut { get; set; }

            Task<HttpResponse<T>> PostData<T>(string url, Dictionary<string, string> data);

            Task<HttpResponse<T>> PostData<T>(string url, string json = "{}");

            Task<HttpResponse<T>> PostData<T>(string url, object data);

            string CreateClientPost(string url, string parameters, int timeOutInMillisecond);

            T CreateClientPost<T>(string url, string parameters, int timeOutInMillisecond);

            string CreateClientGet(string url, Dictionary<string, string> dic);

            T CreateClientGet<T>(string url, Dictionary<string, string> dic);

        }


    }
}
