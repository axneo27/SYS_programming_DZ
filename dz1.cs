
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process.Start("notepad.exe");
            Thread.Sleep(5000);

            Console.WriteLine("Press Enter");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

            Process[] notepads = Process.GetProcessesByName("notepad");
            if (notepads.Length == 0)
            {
                Console.WriteLine("No notepad processes found.");
                return;
            }
            foreach (Process p in notepads)
            {
                p.Kill();
            }
        }
    }
}
