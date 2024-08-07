namespace susalem.vue.Helper
{
    public class ReturnData
    {
        public object dataList { get; set; }
        public int total { get; set; }
    }

    public class ApiResponseData
    {
        public int code { get; set; } = 200;
        public object data { get; set; }
        public string message { get; set; }
    }

    public class PageCom
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 0;
        public int Total { get; set; } = 0;
        public string? Prop { get; set; } = string.Empty;
        public string? Order { get; set; } = string.Empty;//ascending升序//descending降序
    }


    public class ResultHelper
    {
        public static ApiResponseData Success(object res)
        {
            return new ApiResponseData() { code = 200, data = res, message = "success!" };
        }
        public static ApiResponseData Success(object res, string message)
        {
            return new ApiResponseData() { code = 200, data = res, message = message };
        }
        public static ApiResponseData Error(int code, string message)
        {
            return new ApiResponseData() { code = code, message = message };
        }
        public static List<T> DataProcess<T>(List<T> models, PageCom input)
        {
            if (models!=null||models.Count!=0)
            {
                int start = (input.Page - 1) * input.Size;
                int end = ((start + input.Size) >= (models.Count)) ? (models.Count) : (start + input.Size);
                if (start > end)
                {
                    return null;
                }
                models = models.GetRange(start, end - start);
            }
            return models;
        }
    }
}
