using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace parallels
{
    internal class Program
    {

        static void ex1() { 
            List<int> list = new List<int>();
            int n = 1000000;
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < n; i++)
            {
                list.Add(i);
            }
            sw.Stop();
            Console.WriteLine($"For completed operation in {sw.ElapsedMilliseconds}");

            sw.Restart();
            Parallel.For(0, n, i =>
            {
                list.Add(i);
            });
            sw.Stop();
            Console.WriteLine($"Parallel completed operation in {sw.ElapsedMilliseconds}");
        }

        static void ex2()
        {
            Stopwatch sw = new Stopwatch();

            int[] arr = new int[1000000];
            Random randNum = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = randNum.Next(0, 100);
            }

            int s = 0;
            sw.Start();
            Parallel.ForEach(arr, i =>
            {
                s += i;
            });
            sw.Stop();
            Console.WriteLine($"Parallel completed operation in {sw.ElapsedMilliseconds}");
            s = 0;

            sw.Restart();
            foreach (int i in arr) {
                s+= i;
            }
            sw.Stop();
            Console.WriteLine($"For completed operation in {sw.ElapsedMilliseconds}");
            s = 0;
        }

        static void generateRandomArray() {

            int[] arr = new int[1000000];
            Random randNum = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = randNum.Next(0, 100);
            }
        }

        static void NEven() {
            int[] arr = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int n = 0;
            foreach (int i in arr) {
                if (i % 2 == 0) { n++; }
            }
        }

        static void avgArr() {
            int[] arr = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int n = 0;
            foreach (int i in arr)
            {
                n += i;
            }

            float avg = n / arr.Length;
        }

        static void maxarr() {
            int[] arr = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int m = arr[0];
            foreach (int i in arr)
            {
                if (i > m) { m = i; }
            }
        }

        static void ex3()
        {
            Parallel.Invoke(
            ()=>maxarr(),
            ()=>avgArr(),
            ()=>generateRandomArray(),
            ()=>NEven()
            );
        }

        static void ex4() {
            int[] arr = new int[1000];
            Random randNum = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = randNum.Next(0, 100);
            }

            for (int i = 0; i < 5; i++) {
                string fileName = @$"fileTest{i + 1}.txt";
                FileStream fs = File.Create(fileName);

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("The next line!");
                }
            }
        }

        static void Main(string[] args)
        {
            //ex1();
            //ex2();
            //ex3();

            //int n = 1000000000;
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //for (int i = 0; i < n; i++) {
            //    double res = Math.Sqrt(i);
            //}
            //sw.Stop();
            //Console.WriteLine($"For completed operation in {sw.ElapsedMilliseconds}");

            //sw.Restart();
            //while (n > 0) { 
            //    double res = Math.Sqrt(n);
            //    n--;
            //}
            //sw.Stop();
            //Console.WriteLine($"While completed operation in {sw.ElapsedMilliseconds}");

            //sw.Restart();
            //Parallel.For(0, n, i => { 
            //    double res = Math.Sqrt(i); 
            //});
            //sw.Stop();
            //Console.WriteLine($"Parallel completed operation in {sw.ElapsedMilliseconds}");

            //Parallel.Invoke(
            //    () => task(1), 
            //    () => task(2), 
            //    () => task(3), 
            //    () => task(4)
            // );
            //Thread.Sleep(10000);
            //var outer = Task.Factory.StartNew(() => {
            //    Console.WriteLine("Outer task started work!");
            //    var iner = Task.Factory.StartNew(() => {
            //        Console.WriteLine("Inner task started work!");
            //        Thread.Sleep(2000);
            //        Console.WriteLine("Inner task has ended work!");
            //    });
            //    Console.WriteLine("Outer task has finished work");
            //});
            //Console.WriteLine("Main finished work");
            //Console.ReadLine();
        }

        static void task(int i) {
            Console.WriteLine("Task has started work");
            Console.WriteLine($"Task {i} in thread : {Task.CurrentId}");
            Thread.Sleep(2000);
            Console.WriteLine($"Task {i} has finished work");
        }
    }
}
