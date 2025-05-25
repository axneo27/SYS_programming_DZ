using System;
using System.Threading;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dz4
{
    class Program
    {

        static void task(int i, int t) {
            Console.WriteLine("Task " + i + " started");
            Thread.Sleep(t);
            Console.WriteLine("Task " + i + " finished");
        }
        
        static void ex1() {
            Task t1 = new Task(() => task(1, 1000));
            Task t2 = new Task(() => task(2, 2000));
            Task t3 = new Task(() => task(3, 3000));
            t1.Start();
            t2.Start();
            t3.Start();

            Task[] tasks = { t1, t2, t3 };
            Task t4 = Task.WhenAll(tasks);
            t4.ContinueWith((t) => {
                Console.WriteLine("All tasks completed");
            });
        }

        static int randomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        static void ex2()
        {
            Task<int> t1 = Task.Run(() => randomNumber(1, 10));
            t1.ContinueWith((t) => {
                if (t.Result > 5)
                {
                    Console.WriteLine("Square: " + (t.Result * t.Result));
                }
                else
                {
                    Console.WriteLine("Too small number: " + t.Result);
                }
            });
        }

        static void ex3() {
            Task t = Task.Run(() => { throw new Exception(); });
            t.ContinueWith((t) => {
                if (t.IsFaulted)
                {
                    Console.WriteLine("Exception: " + t.Exception.InnerException.Message);
                }
            });
        }

        static async Task<int> Factorial(int n)
        {
            Task<int> task = Task.Run(() => {
                int result = 1;
                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }
                return result;
            });
            return await task;
        }

        static async void ex4()
        {
            Task<int> t = Factorial(10);
            Console.WriteLine(t.Result);
        }

        static void ex5()
        {
            Console.WriteLine("Main thread is doing some work...");
            Task.Run(() => {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(i + 1);
                    Thread.Sleep(1000);
                }
            });
        }

        static void Main(string[] args)
        {
            // ex1();
            // Console.ReadKey();
            
            // ex2();
            // Console.ReadKey();

            // ex3();
            // Console.ReadKey();

            // ex4();

            // ex5();
            // Console.ReadKey();
        }
    }

}
