using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.HubConfig
{
    public class MyHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("MessageResponse",message);
            //await Clients.Client(Context.ConnectionId).SendAsync("MessageResponse", message);
        }
    }
}
