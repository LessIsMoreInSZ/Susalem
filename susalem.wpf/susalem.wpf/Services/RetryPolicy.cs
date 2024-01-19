using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Services
{
    public class RetryPolicy : IRetryPolicy
    {
        /// <summary>
        /// 重连规则：重连次数<50：间隔1s;重试次数<250:间隔30s;重试次数>250:间隔1m
        /// </summary>
        /// <param name="retryContext"></param>
        /// <returns></returns>
        TimeSpan? IRetryPolicy.NextRetryDelay(RetryContext retryContext)
        {
            var count = retryContext.PreviousRetryCount / 50;
            //MessageBox.Show("wulala"+count.ToString());
            //Trace.WriteLine(count.ToString());
            if (count < 1)//重试次数<50,间隔1s
            {
                return new TimeSpan(0, 0, 3);
            }
            else if (count < 5)//重试次数<250:间隔30s
            {
                return new TimeSpan(0, 0, 30);
            }
            else //重试次数>250:间隔1m
            {
                return new TimeSpan(0, 1, 0);
            }
        }
    }
}
