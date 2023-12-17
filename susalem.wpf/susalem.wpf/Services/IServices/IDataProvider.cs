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

        Task<HttpResponse<T>> AddAsync<T>();

        Task<HttpResponse> GetAsync(string url);

        Task<HttpResponse> DeleteAsync(string url);

        Task<HttpResponse<T>> PostData<T>(string url, string json = "{}");

        Task<HttpResponse<T>> PostData<T>(string url, object data);


    }
}
