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
        string Url { get; set; }
        TimeSpan TimeOut { get; set; }

        Task<HttpResponse<T>> PostData<T>(string url, Dictionary<string, string> data);

        Task<HttpResponse<T>> PostData<T>(string url, string json = "{}");

        Task<HttpResponse<T>> PostData<T>(string url, object data);

        ResultModel<string> CreateClientPost(string url, string parameters, int timeOutInMillisecond);

        ResultModel<T> CreateClientPost<T>(string url, string parameters, int timeOutInMillisecond);

        Task<ResultModel<T>> CreateClientPostAsync<T>(string url, string parameters);

        ResultModel<string> CreateClientGet(string url, Dictionary<string, string> dic, int timeOutInMillisecond);

        ResultModel<T> CreateClientGet<T>(string url, Dictionary<string, string> dic, int timeOutInMillisecond);

        Task<ResultModel<T>> CreateClientGetAsync<T>(string url, Dictionary<string, string> dic);

    }
}
