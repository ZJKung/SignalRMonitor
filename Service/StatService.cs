using System.Diagnostics;

namespace SignalRMonitor.Service
{
    public class StatService
    {
        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        public StatService()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public string getCurrentCpuUsage()
        {
            return cpuCounter.NextValue() + "%";
        }

        public string getAvailableRAM()
        {
            return ramCounter.NextValue() + "MB";
        }
    }
}