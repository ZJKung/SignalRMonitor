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

                await Clients.All.SendAsync("SendMonitorData", JsonSerializer.Serialize(
                                new
                                {
                                    CPU = cpuUsage,
                                    RAM = memUsage
                                }));
                await Task.Delay(1000);
            }
        }
        public Task UseCPU()
        {
            Console.WriteLine("c");
            var timeout = DateTime.Now.AddSeconds(5);
            while (DateTime.Now.CompareTo(timeout) < 0)
            {
                var newGuid = Guid.NewGuid();
            }
            return Task.CompletedTask;
        }
        public Task UseMemory()
        {
            Console.WriteLine("m");
            byte[] buff = new byte[10 * 1024 * 1024];
            for (var i = 0; i < buff.Length; i++) buff[i] = (byte)(i % 256);
            //MemoryMonster.Add(buff);
            //Sleep 5 seconds to keep the memory space "active"
            Task.Delay(5000);
            return Task.CompletedTask;
        }
    }
}