using System;

namespace dz3
{

    public class Stat {
        public int wordsCount;
        public int linesCount;
        public int signsCount;

        public void TextAnalyze(string fileDirectory) {
            lock (this) {
                char[] signs = {'.', ',', '!', '?', ':', ';', '-', '_', '(', ')', '[', ']', '{', '}', '\'', '"'};

                string text = File.ReadAllText(fileDirectory);
                Console.WriteLine(text);

                string[] lines = text.Split("\n");
                linesCount += lines.Length;
                
                
                foreach (string line in lines) {
                    string[] words = line.Split(' ');
                    wordsCount += words.Length;
                    foreach (string word in words) {
                        foreach (char sign in signs) {
                            if (word.Contains(sign)) {
                                signsCount++;
                            }
                        }
                    }
                }
                // Console.WriteLine($"Lines count: {linesCount}");
                // Console.WriteLine($"Words count: {wordsCount}");
                // Console.WriteLine($"Signs count: {signsCount}");
            }
        }

    }

    public class LockCounter {
        public int evenCount;
        public int oddCount;

        public void Count() {
            //Використати Interlocked для потокобезпечного збільшення
            Monitor.Enter(this);
            try {
                for (int i = 0; i < 10000; i++) {
                    if (i % 2 == 0) {
                        Interlocked.Increment(ref evenCount);
                    } else {
                        Interlocked.Increment(ref oddCount);
                    }
                }
            } finally {
                Monitor.Exit(this);
            }
        }
    }

    // ex4
    public class ThreadSafeCounter {
        private int evenCount;
        private int oddCount;
        private readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();

        public int EvenCount
        {
            get
            {
                readerWriterLockSlim.EnterReadLock();
                try
                {
                    return evenCount;
                }
                finally
                {
                    readerWriterLockSlim.ExitReadLock();
                }
            }
        }

        public int OddCount
        {
            get
            {
                readerWriterLockSlim.EnterReadLock();
                try
                {
                    return oddCount;
                }
                finally
                {
                    readerWriterLockSlim.ExitReadLock();
                }
            }
        }

        public void Count()
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                for (int i = 0; i < 10000; i++)
                {
                    if (i % 2 == 0)
                    {
                        evenCount++;
                    }
                    else
                    {
                        oddCount++;
                    }
                }
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            // Stat stat = new Stat();

            // string f1 = @"f1.txt";
            // string f2 = @"f2.txt";

            // Thread t1 = new Thread(() => stat.TextAnalyze(f1));
            // t1.Start();
            // Thread t2 = new Thread(() => stat.TextAnalyze(f2));
            // t2.Start();

            // t1.Join();
            // t2.Join();

            // Console.WriteLine($"Lines count: {stat.linesCount}");
            // Console.WriteLine($"Words count: {stat.wordsCount}");
            // Console.WriteLine($"Signs count: {stat.signsCount}");
        }
    }
}
