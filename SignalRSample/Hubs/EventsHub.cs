using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRSample.Hubs
{
    public class EventsHub : Hub
    {
        public async Task SendEvent(string eventdata)
        {
            await Clients.All.SendAsync("ReceiveEvent", eventdata);
        }
    }
}
