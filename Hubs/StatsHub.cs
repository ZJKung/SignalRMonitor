using Microsoft.AspNetCore.SignalR;
using SignalRMonitor.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
namespace SignalRMonitor.Hubs
{
    public class StatsHub : Hub
    {
        private StatService _statService;
        public StatsHub(StatService statService)
        {
            _statService = statService;
        }
        public async Task GetMonitor()
        {
            while (1 == 1)
            {
                await Clients.All.SendAsync("SendMonitorData", JsonSerializer.Serialize(new { CPU = _statService.getCurrentCpuUsage(), RAM = _statService.getAvailableRAM() }));
                await Task.Delay(1000);
            }
            // while(!cancellationToken.IsCancellationRequested){

            // }
        }
    }
}