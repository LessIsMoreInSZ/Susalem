using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using susalem.wpf.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace susalem.wpf.Services
{
    internal class SignalrService : ISignalrService
    {
        public IServiceProvider Services { get; set; }
        public HubConnection HubConnection { get; set; }

        public HubConnectionBuilder HubConnectionBuilder { get; set; }

        public HubConnection Get(string url)
        {
            HubConnectionBuilder = new HubConnectionBuilder();
            HubConnectionBuilder.WithUrl(url);
            HubConnection hubConnection = HubConnectionBuilder.Build();
            return hubConnection;
        }

        public event Action<string, string, bool> NewTextMessage;

        public void Connect(string url)
        {
            try
            {
                // 创建 SignalR 连接
                HubConnection = new HubConnectionBuilder()
                    .WithUrl(url)
                    .WithAutomaticReconnect(new RetryPolicy())
                    .Build();

                HubConnection.Closed += async (error) =>
                {
                    await Task.Delay(1 * 1000);
                    await HubConnection.StartAsync();
                };
                HubConnection.On<string, string>("SendMessage", (user, message) =>
                {
                    try
                    {
                        
                    }
                    catch (Exception ex)
                    {
                        
                    }
                });

                Task.Run(() =>
                {
                    while (HubConnection.State != HubConnectionState.Connected)
                    {
                        Thread.Sleep(3 * 1000);
                        HubConnection.StartAsync();
                    }
                });
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        public void Disconnect()
        {
            HubConnection?.StopAsync().Wait();
        }

        public void Send(string user, string message)
        {
            //HubConnection?.InvokeAsync("SendMessage", user, message).Wait();
        }
    }
}
