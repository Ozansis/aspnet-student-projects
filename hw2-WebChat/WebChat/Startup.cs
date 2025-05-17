using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(WebChat.Startup))]

namespace WebChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(); // 💥 işte burada SignalR'ı aktif ediyoruz
        }
    }
}
