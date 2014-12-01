using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using WebViewPerformanceCounter.Core;
using WebViewPerformanceCounter.Core.Configuration;

namespace Web.Controllers
{
    public class PCounterController : ApiController
    {
        //public List<ServerResult> GetData()
        //{
        //    return PCounter.GetResults();
        //}

        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        private async Task ProcessWSChat(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            var pcConuter = new PCounter();
            while (true)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                //WebSocketReceiveResult result = await socket.ReceiveAsync(
                //    buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    Thread.Sleep(CManager.PCWebViewer.WebSocketSleep);
                    var r = pcConuter.GetResults();

                    var scriptSerializer = JsonSerializer.Create(new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Error
                    });

                    string rs = null;
                    using (var sw = new StringWriter())
                    {
                        scriptSerializer.Serialize(sw, r);
                        rs = sw.ToString();
                    }

                    buffer = new ArraySegment<byte>(
                        Encoding.UTF8.GetBytes(rs));
                    await socket.SendAsync(
                        buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
