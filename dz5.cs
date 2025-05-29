using System.Diagnostics;

namespace dz5
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

        static void createFile(int i) {
            string fileName = @$"fileTest{i + 1}.txt";

            int[] arr = new int[100];
            Random randNum = new Random();
            for (int j = 0; j < arr.Length; j++)
            {
                arr[j] = randNum.Next(0, 100);
            }

            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                foreach (int j in arr)
                {
                    outputFile.WriteLine(j);
                }
            }
        }

        static void createFiles() {
            for (int i = 0; i < 5; i++)
            {
                createFile(i);
            }
        }
        static void ex4() {
            Parallel.ForEach(Enumerable.Range(0, 5), i =>
            {
                readFile(@$"fileTest{i + 1}.txt");
            });
        }

        static void readFile(string fileName) {
            int lines = 0;
            using (StreamReader inputFile = new StreamReader(fileName))
            {
                while (inputFile.ReadLine() != null)
                {
                    lines++;
                }
            }
            Console.WriteLine($"File {fileName} has {lines} numbers.");
        }

        static void checkBalance(int i) {
            System.Console.WriteLine("Checking balance for client " + i);
            Thread.Sleep(1000); 
        }

        static void giveCash(int i) {
            System.Console.WriteLine("Giving cash for client " + i);
            Thread.Sleep(1000);
        }

        static void topUpAccount(int i) {
            System.Console.WriteLine("Topping up account for client " + i);
            Thread.Sleep(1000);
        }

        static void doAllOperations(int i)
        {
            Parallel.Invoke(
                () => checkBalance(i),
                () => giveCash(i),
                () => topUpAccount(i)
            );
        }

        static void ex5()
        {
            for (int i = 1; i <= 5; i++)
            {
                doAllOperations(i);
            }
        }

        static void Main(string[] args)
        {
            //ex1();
            //ex2();
            //ex3();

            // createFiles();

            // ex4();
            ex5();
        }
    }
}
