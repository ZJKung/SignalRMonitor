using System;
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
            ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);
        }
        public float getCurrentCpuUsage()
        {
            return cpuCounter.NextValue();
        }

        public float getUsageRAM()
        {
            var gcMemoryInfo = GC.GetGCMemoryInfo();
            //get total mb in byte
            var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
            // it will give the size of memory in MB
            var physicalMemory = (float)(installedMemory / 1048576.0);
            return physicalMemory - ramCounter.NextValue();
        }
    }
}