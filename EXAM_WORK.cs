using Microsoft.Win32;

namespace SYS_EXAM_WORK
{
    public class RegistryHelper
    {
        public static bool RegistryKeyExists(string keyPath)
        {
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(keyPath);
                return key != null;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
        }
    }

    public class SystemMonitor
    {
        private const string BaseKeyPath = @"Software\SystemMonitor";
        public RegistryKey systemMonitorKey;

        public SystemMonitor()
        {
            RegistryKey softwareKey = null;
            try
            {
                softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
                systemMonitorKey = softwareKey.CreateSubKey("SystemMonitor", true);

                if (systemMonitorKey == null)
                {
                    throw new Exception("SystemMonitor registry key could not be created or opened.");
                }

                CreateKeyValue_DWORD("LogEnabled", 1);
                CreateKeyValue_DWORD("ParallelEnabled", 1);
                CreateKeyValue_DWORD("MonitoringInterval", 1000);
            }
            finally
            {
                if (softwareKey != null)
                {
                    softwareKey.Close();
                }
            }
        }

        public async void AnalyzeSystemPerformance()
        {
            Console.WriteLine("Analyzing system performance...");
            int[] ints = GeneratePseudoLoad();
            foreach (var load in ints)
            {
                Console.WriteLine(load);
            }
            unsafe {
                fixed (int* ptr = ints)
                {
                    double average = CalcAverage(ptr, ints.Length);
                    Console.WriteLine($"Average Load: {average}");
                }
            }
            if (IsLogEnabled())
            {
                Console.WriteLine("Logging is enabled. Performance data will be logged.");
                string data = "";
                foreach (var load in ints)
                {
                    data += $"{load}\n";
                }
                await WriteDataToLog(data);
            }
            else
            {
                Console.WriteLine("Logging is disabled. No performance data will be logged.");
            }
        }

        public async Task WriteDataToLog(string data)
        {
            try
            {
                string logFilePath = "monitor_log.txt";
                await File.AppendAllTextAsync(logFilePath, $"{DateTime.Now}:\n{data}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}", ex);
            }
        }

        public bool IsLogEnabled()
        {
            try
            {
                int value = (int)systemMonitorKey.GetValue("LogEnabled", 0);
                return value == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading LogEnabled value: {ex.Message}", ex);
            }
        }

        public bool SystemMonitorRegistryExists()
        {
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(BaseKeyPath);
                return key != null;
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
        }

        public void CreateKeyValue_DWORD(string key, int value)
        {
            RegistryKey writeableKey = null;
            try
            {
                writeableKey = Registry.CurrentUser.OpenSubKey(BaseKeyPath, true);
                if (writeableKey != null)
                {
                    writeableKey.SetValue(key, value, RegistryValueKind.DWord);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing registry value {key}: {ex.Message}", ex);
            }
            finally
            {
                if (writeableKey != null)
                {
                    writeableKey.Close();
                }
            }
        }

        public int[] GeneratePseudoLoad() {
            Random random = new Random();
            int[] loads = new int[100]; //
            if (IsParallelEnabled())
            {
                Console.WriteLine("Parallel enabled");
                Parallel.For(0, loads.Length, i =>
                {
                    loads[i] = random.Next(0, 100);
                });
                return loads;
            }
            else {
                for (int i = 0; i < loads.Length; i++)
                {
                    loads[i] = random.Next(0, 100);
                }
                return loads;
            }
        }

        public bool IsParallelEnabled()
        {
            try
            {
                int value = (int)systemMonitorKey.GetValue("ParallelEnabled", 0);
                return value == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading ParallelEnabled value: {ex.Message}", ex);
            }
        }

        public int GetMonitoringInterval() {
            try
            {
                return (int)systemMonitorKey.GetValue("MonitoringInterval", 1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading MonitoringInterval value: {ex.Message}", ex);
                return 1000;
            }
        }

        public unsafe static double CalcAverage(int* data, int size) { 
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += data[i];
            }
            if (size == 0)
            {
                return 0.0;
            }
            return (double)sum / size;
        }
    }

    internal class Program
    {
        static void Main()
        {
            try
            {
                SystemMonitor systemMonitor = new SystemMonitor();
                Console.WriteLine("SystemMonitor initialized successfully.");

                int monitoringInterval = systemMonitor.GetMonitoringInterval();
                Console.WriteLine($"Monitoring Interval: {monitoringInterval} ms");
                while (true)
                {
                    systemMonitor.AnalyzeSystemPerformance();
                    Thread.Sleep(monitoringInterval);
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Exiting System Monitor.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}
