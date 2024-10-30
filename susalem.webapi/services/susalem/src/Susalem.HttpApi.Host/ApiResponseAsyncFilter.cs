using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Susalem;

public class ApiResponseAsyncFilter : IAsyncAlwaysRunResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // 处理结果
        if (context.Result is ObjectResult objectResult)
        {
            var response = ApiResponse<object>.Success(objectResult.Value);
            context.Result = new JsonResult(response);
        }
        else if (context.Result is BadRequestObjectResult badRequestResult)
        {
            var response = ApiResponse<object>.Error(400, badRequestResult.Value?.ToString());
            context.Result = new JsonResult(response);
        }
        // 可以根据需要添加其他状态码的处理

        // 先执行下一步
        await next();
    }
}