using Microsoft.AspNetCore.SignalR;
using SignalRMonitor.Service;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Logging;
using System;

namespace SignalRMonitor.Hubs
{
    public class StatsHub : Hub
    {
        private StatService _statService;


        public StatsHub(StatService statService)
        {
            _statService = statService;
            //_logger = logger;
        }
        public async Task GetMonitor()
        {
            while (true)
            {
                float cpuUsage = _statService.getCurrentCpuUsage();

                float memUsage = _statService.getUsageRAM();

                //System.Console.WriteLine($"CPU : {cpuUsage} RAM : {memUsage}");

                await Clients.Client(Context.ConnectionId).SendAsync("SendMonitorData", JsonSerializer.Serialize(
                                new
                                {
                                    CPU = cpuUsage,
                                    RAM = memUsage
                                }));
                await Task.Delay(1000);
            }
        }
    }
}