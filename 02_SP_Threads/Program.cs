namespace _02_SP_Threads
{
    internal class Program
    {
        //1
        static void Method()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"\t\t\t{i}");
                Thread.Sleep(100);
            }
        }

        //2

        static void Method2(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"\t\t\t{i}");
                Thread.Sleep(100);
            }
        }

        //3

        static void Method3(int start3, int end3, int threadNumber3)
        {
            for (int i = start3; i <= end3; i++)
            {
                Console.WriteLine($"Thread {threadNumber3}: {i}");
                Thread.Sleep(50);
            }
        }

        //4

        private static int[] numbers = new int[10000];
        private static int max;
        private static int min;
        private static double average;
        private static readonly object lockObject = new object();


        static void Main(string[] args)
        {

            //1

            Thread thread = new Thread(Method);
            thread.Start();

            //2

            Console.WriteLine("Enter thread's start:");
            int start = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter thread's end:");
            int end = int.Parse(Console.ReadLine()!);

            Thread thread2 = new Thread(() => Method2(start, end));
            thread2.Start();

            //3

            Console.WriteLine("How many thread do you wont?:");
            int threadCount3 = int.Parse(Console.ReadLine()!);

            Thread[] threads3 = new Thread[threadCount3];

            for (int i = 0; i < threadCount3; i++)
            {
                Console.WriteLine($"Start theard: {i + 1}:");
                int startRange3 = int.Parse(Console.ReadLine()!);

                Console.WriteLine($"End theard {i + 1}:");
                int endRange3 = int.Parse(Console.ReadLine()!);

                int localStart3 = startRange3;
                int localEnd3 = endRange3;
                int localThreadNumber3 = i + 1;

                threads3[i] = new Thread(() => Method3(localStart3, localEnd3, localThreadNumber3));
                threads3[i].Start();
            }

            foreach (var thread3 in threads3)
            {
                thread.Join();
            }


            Console.WriteLine("End Program.");

            //4

            Random random = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(0, 100);
            }


            Thread maxThread = new Thread(() =>
            {
                max = numbers[0];
                foreach (int number in numbers)
                {
                    if (number > max)
                    {
                        max = number;
                    }
                }
            });

            Thread minThread = new Thread(() =>
            {
                min = numbers[0];
                foreach (int number in numbers)
                {
                    if (number < min)
                    {
                        min = number;
                    }
                }
            });

            Thread avgThread = new Thread(() =>
            {
                double sum = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                }
                average = sum / numbers.Length;
            });

            //5

            Thread printThread = new Thread(() =>
            {
                Console.WriteLine("Numbers:");
                foreach (int number in numbers)
                {
                    Console.WriteLine(number);
                }
            });

            maxThread.Start();
            minThread.Start();
            avgThread.Start();
            printThread.Start();

            maxThread.Join();
            minThread.Join();
            avgThread.Join();
            printThread.Join();

            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"AVG: {average}");
        }
    }
}
