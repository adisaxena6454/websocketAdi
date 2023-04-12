using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SocketIOClient;

namespace WebSocketWebApp.Helper
{
    public class WebSocketHelper
    {
        public async Task<string> getSocketDataAsync()
        {
            var client = new SocketIO("ws://13.232.18.39/");
            string socketRsp = string.Empty;
            await client.ConnectAsync();
            client.OnConnected += async (sender, e) =>
            {
               client.ListenersAny();
            };
            client.On("dashboard", response =>
            {
                Console.WriteLine(response.ToString());
                socketRsp = response.ToString();
            });
            return socketRsp;
        }
    }
}