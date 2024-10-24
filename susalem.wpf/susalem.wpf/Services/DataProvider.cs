using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using susalem.wpf.Models.HttpModels;
using susalem.wpf.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Services
{
    public class ApiDataProvider : IDataProvider
    {
        private HttpClient _httpClient;
        //private Logger _logger;
        public ApiDataProvider()
        {

        }
        public ApiDataProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            HttpClient client = new HttpClient();
        }
        #region 设置信息
        public string Url { get; set; }
        //public IAppHeader Header { get; set; }
        public TimeSpan TimeOut { get; set; }

        #endregion

        #region 密匙模式
        public void Init(string url, string appId, string appSecret, TimeSpan timeout)
        {
            //var header = new AppSecretHeader(appId, appSecret);
            Url = url;
            //Header = header;
            TimeOut = timeout;
        }
        #endregion

        #region Token模式
        public void Init(string url, string userName, string password, int headMode, TimeSpan timeout)
        {
            //var header = new AppTokenHeader(userName, password);
            Url = url;
            //Header = header;
            TimeOut = timeout;
        }
        #endregion


        public async Task<HttpResponse<T>> PostData<T>(string url, Dictionary<string, string> data)
        {
            try
            {
                if (!url.StartsWith("http"))
                {
                    url = Url + url;
                }
                MultipartFormDataContent stringContent = null;
                if (data != null)
                {
                    stringContent = new MultipartFormDataContent();

                    foreach (var item in data)
                    {
                        stringContent.Add(new StringContent(item.Value), item.Key);
                    }
                }
                var content = await PostAsync(url, content: stringContent, TimeOut);
                var result = JsonConvert.DeserializeObject<HttpResponse<T>>(content);
                return result;
            }
            catch (Exception ex)
            {
                return new HttpResponse<T>() { Msg = ex.Message, Success = false };
            }
        }


        public async Task<HttpResponse<T>> PostData<T>(string url, string json)
        {
            try
            {
                if (!url.StartsWith("http"))
                {
                    url = Url + url;
                }
                var content = await PostAsyncJson(url, json, TimeOut);
                var result = JsonConvert.DeserializeObject<HttpResponse<T>>(content);
                return result;
            }
            catch (Exception ex)
            {
                return new HttpResponse<T>() { Msg = ex.Message, Success = false };
            }
        }


        public async Task<HttpResponse<T>> PostData<T>(string url, object data)
        {
            return await PostData<T>(url, JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="primaryKeyColumn">类型的主键，目前系统都是id</param>
        /// <param name="ids">需要删除的id列表</param>
        /// <returns></returns>
        public async Task<HttpResponse> UploadFile(string path, string fileName, string remark)
        {
            try
            {
                var data = new MultipartFormDataContent();
                ////添加字符串参数，参数名为qq
                //data.Add(new StringContent(qq), "qq");

                //添加文件参数，参数名为files，文件名为123.png
                data.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", fileName);

                var content = await PostAsync(string.Format("{0}/api/FileServer/SaveFile", Url), data, TimeOut);
                var result = JsonConvert.DeserializeObject<HttpResponse>(content);
                return result;
            }
            catch (Exception ex)
            {
                return new HttpResponse() { Msg = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="primaryKeyColumn">类型的主键，目前系统都是id</param>
        /// <param name="ids">需要删除的id列表</param>
        /// <returns></returns>
        public async Task<HttpResponse> DownLoadFile(string fullpath, string savepath)
        {
            try
            {
                FileStream fs = null;
                try
                {
                    var content = await GetByteArrayAsync(fullpath, TimeOut);
                    fs = new FileStream(savepath, FileMode.Create);
                    fs.Write(content, 0, content.Length);
                    return new HttpResponse() { Success = true };
                }
                catch (Exception ex)
                {
                    return new HttpResponse() { Success = false, Msg = ex.ToString() };
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }
            catch (Exception ex)
            {
                return new HttpResponse() { Msg = ex.Message, Success = false };
            }
        }


        #region HttpClient
        /// <summary>
        /// 使用post方法异步请求
        /// </summary>
        /// <param name="url">目标链接</param>
        /// <param name="json">发送的参数字符串，只能用json</param>
        /// <returns>返回的字符串</returns>
        public async Task<string> PostAsyncJson(string url, string json, TimeSpan timeSpan)
        {
            _httpClient.Timeout = timeSpan;
            HttpContent content = new StringContent(json);
            string responseBody = string.Empty;
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("000");
                //throw ex;
            }

            return responseBody;
        }

        public async Task<string> Post(string url, string content)
        {
            string result = string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(content);
            HttpWebRequest r = HttpWebRequest.Create(url) as HttpWebRequest;
            r.ContentType = "application/json";
            r.Method = "POST";
            r.ContentLength = data.Length;
            using (Stream s = r.GetRequestStream())
            {
                s.Write(data, 0, data.Length);
            }
            using (Stream s = r.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(s);
                result = reader.ReadToEnd();
            }
            return result;


        }
        /// <summary>
        /// 使用post方法异步请求
        /// </summary>
        /// <param name="url">目标链接</param>
        /// <param name="data">发送的参数字符串</param>
        /// <returns>返回的字符串</returns>
        public async Task<string> PostAsync(string url, HttpContent content, TimeSpan timeSpan)
        {
            _httpClient.Timeout = timeSpan;

            string responseBody = string.Empty;
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseBody;
        }

        /// <summary>
        /// 使用post方法异步请求
        /// </summary>
        /// <param name="url">目标链接</param>
        /// <param name="data">发送的参数字符串</param>
        /// <returns>返回的字符串</returns>
        public async Task<string> PostAsync(string url, string data, TimeSpan timeSpan, Dictionary<string, string> header = null)
        {
            _httpClient.Timeout = timeSpan;
            HttpContent content = new StringContent(data);
            if (header != null)
            {
                _httpClient.DefaultRequestHeaders.Clear();
                foreach (var item in header)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            string responseBody = string.Empty;
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseBody;
        }

        public async Task<byte[]> GetByteArrayAsync(string uri, TimeSpan timeSpan, Dictionary<string, string> header = null)
        {
            _httpClient.Timeout = timeSpan;
            if (header != null)
            {
                _httpClient.DefaultRequestHeaders.Clear();
                foreach (var item in header)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            byte[] urlContents = await _httpClient.GetByteArrayAsync(uri);
            return urlContents;
        }

        /// <summary>
        /// 使用get方法异步请求
        /// </summary>
        /// <param name="url">目标链接</param>
        /// <returns>返回的字符串</returns>
        public async Task<string> GetAsync(string url, TimeSpan timeSpan, Dictionary<string, string> header = null)
        {
            _httpClient.Timeout = timeSpan;
            if (header != null)
            {
                _httpClient.DefaultRequestHeaders.Clear();
                foreach (var item in header)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            string responseBody = string.Empty;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();//用来抛异常的
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseBody;
        }
        #endregion

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="timeOutInMillisecond"></param>
        /// <returns></returns>
        public ResultModel<string> CreateClientPost(string url, string parameters, int timeOutInMillisecond = 15000)
        {
            string httpResult = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(parameters);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.ContentType.CharSet = "utf-8";
                    Task<HttpResponseMessage> task = client.PostAsync(url, content);
                    #region 超时
                    if (task.Wait(timeOutInMillisecond))
                    {
                        HttpResponseMessage response = task.Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Task<string> result = response.Content.ReadAsStringAsync();
                            //result.Wait();
                            httpResult = result.Result;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<string>() { Success = false };
            }
            return new ResultModel<string>() { Success = true, Data = httpResult };
        }


        public ResultModel<T> CreateClientPost<T>(string url, string parameters, int timeOutInMillisecond = 15000)
        {
            ResultModel<T> tHttpResult = new ResultModel<T>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(parameters);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.ContentType.CharSet = "utf-8";
                    Task<HttpResponseMessage> task = client.PostAsync(url, content);
                    HttpResponseMessage response = task.Result;
                    if (task.Wait(timeOutInMillisecond))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Task<string> result = response.Content.ReadAsStringAsync();
                            //result.Wait();
                            tHttpResult = JsonConvert.DeserializeObject<ResultModel<T>>(result.Result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<T>() { Success = false };
            }

            return tHttpResult;
        }


        public async Task<ResultModel<T>> CreateClientPostAsync<T>(string url, string parameters)
        {
            ResultModel<T> tHttpResult = new ResultModel<T>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(parameters);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.ContentType.CharSet = "utf-8";
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        //result.Wait();
                        tHttpResult = JsonConvert.DeserializeObject<ResultModel<T>>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<T>() { Success = false };
            }

            return tHttpResult;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dic">请求参数定义</param>
        /// <returns></returns>
        public ResultModel<string> CreateClientGet(string url, Dictionary<string, string> dic, int timeOutInMillisecond = 15000)
        {
            string httpResult = string.Empty;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(url);
                if (dic.Count > 0)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in dic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    Task<HttpResponseMessage> task = client.GetAsync(builder.ToString());
                    if (task.Wait(timeOutInMillisecond))
                    {
                        HttpResponseMessage response = task.Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            httpResult = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<string> { Success = false };
            }
            return new ResultModel<string> { Data = httpResult, Success = true };
        }

        public ResultModel<T> CreateClientGet<T>(string url, Dictionary<string, string> dic, int timeOutInMillisecond = 15000)
        {
            ResultModel<T> tHttpResult = new ResultModel<T>();
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(url);
                if (dic.Count > 0)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in dic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    Task<HttpResponseMessage> task = client.GetAsync(builder.ToString());
                    if (task.Wait(timeOutInMillisecond))
                    {
                        HttpResponseMessage response = task.Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string taskResponse = response.Content.ReadAsStringAsync().Result;
                            //taskResponse.Wait();
                            tHttpResult = JsonConvert.DeserializeObject<ResultModel<T>>(taskResponse);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return new ResultModel<T>() { Success = false };
            }


            return tHttpResult;
        }

        public async Task<ResultModel<T>> CreateClientGetAsync<T>(string url, Dictionary<string, string> dic)
        {
            ResultModel<T> tHttpResult = new ResultModel<T>();
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(url);
                if (dic.Count > 0)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in dic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(builder.ToString());
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string taskResponse = await response.Content.ReadAsStringAsync();
                        tHttpResult = JsonConvert.DeserializeObject<ResultModel<T>>(taskResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<T>() { Success = false };
            }

            return tHttpResult;
        }

    }
}
