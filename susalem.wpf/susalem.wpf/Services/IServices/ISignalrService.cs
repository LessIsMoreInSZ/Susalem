using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Services.IServices
{
    public interface ISignalrService
    {
        HubConnection HubConnection { get; set; }

        void Connect(string url);

        void Disconnect();

        void Send(string user, string message);
    }
}
