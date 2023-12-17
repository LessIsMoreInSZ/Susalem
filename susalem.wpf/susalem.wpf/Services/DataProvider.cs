using susalem.wpf.Models.HttpModels;
using susalem.wpf.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Services
{
    internal class DataProvider : IDataProvider
    {
        HttpClient _httpClient;
        string IDataProvider.Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        TimeSpan IDataProvider.TimeOut { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Task<HttpResponse<T>> IDataProvider.AddAsync<T>()
        {
            throw new NotImplementedException();
        }

        Task<HttpResponse> IDataProvider.DeleteAsync(string url)
        {
            throw new NotImplementedException();
        }

        Task<HttpResponse> IDataProvider.GetAsync(string url)
        {
            throw new NotImplementedException();
        }

        Task<HttpResponse<T>> IDataProvider.PostData<T>(string url, string json)
        {
            throw new NotImplementedException();
        }

        Task<HttpResponse<T>> IDataProvider.PostData<T>(string url, object data)
        {
            throw new NotImplementedException();
        }
    }
}
