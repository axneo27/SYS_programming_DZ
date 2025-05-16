using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace dz2
{
    class Program
    {
        //1
        static void ThreadTask(string name, ThreadPriority Priority)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{name} - {i + 1}");
                    Thread.Sleep(100);
                }
            });
            thread.Name = name;
            thread.Priority = Priority;
            thread.Start();
        }

        //2 
        static void Task1() {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Task 1AAAAA");
                Thread.Sleep(100);
            }
        }

        static void Task2() {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Task 2BBBBBB");
                Thread.Sleep(100);
            }
        }

        static void ex2()
        {
            Thread thread1 = new Thread(Task1);
            Thread thread2 = new Thread(Task2);
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
        }

        public class ThreadParams
        {
            public string id { get; set; }
            public int sleepTime { get; set; }

            public ThreadParams(string id, int sleepTime)
            {
                this.id = id;
                this.sleepTime = sleepTime;
            }
        }

        static void ThreadFunk(ThreadParams threadParams)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{threadParams.id} - {i + 1}");
                Thread.Sleep(threadParams.sleepTime);
            }
        }

        static void Main()
        {
            // ThreadTask("Thread 1", ThreadPriority.Highest);
            // ThreadTask("Thread 2", ThreadPriority.Normal);
            // ThreadTask("Thread 3", ThreadPriority.Lowest);
            // ThreadTask("Thread 4", ThreadPriority.BelowNormal);
            // ThreadTask("Thread 5", ThreadPriority.AboveNormal);

            //ex 4
            ThreadParams threadParams1 = new ThreadParams("Thread 1", 100);
            ThreadParams threadParams2 = new ThreadParams("Thread 2", 200);

            Thread thread1 = new Thread(() => ThreadFunk(threadParams1));
            Thread thread2 = new Thread(() => ThreadFunk(threadParams2));
            thread1.Start();
            thread2.Start();
            
            
        }
    }
}
