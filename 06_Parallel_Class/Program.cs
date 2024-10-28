using System.Text.Json;

namespace _06_Parallel_Class
{
    internal class Program
    {
        static int Factorial(int num)
        {
            int result = 1;
            for (int i = 1; i <= num; i++)
            {
                result *= i;
            }
            return result;
        }
        static void FactorialVoid(int num)
        {
            int result = 1;
            for (int i = 1; i <= num; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Factorial {num}: {result}");

        }
        static int FactorialCounter(int num)
        {
            string factorial = Factorial(num).ToString();
            int result = factorial.Count();
            return result;

        }
        static int FactorialMembersSumma(int num)
        {
            List<int> members = new List<int>();
            string factorial = Factorial(num).ToString();
            foreach (char member in factorial)
            {
                members.Add(int.Parse(member.ToString()));
            }
            return members.Sum();
        }
        static void MultiplicationTable(int num)
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{i} * {num} = {i * num}");
            }
            Console.WriteLine("***********************************************");
        }
        static List<int> SerializeDeserialize(List<int> arr)
        {
            foreach (int i in arr)
            {
                Console.Write($"[{i}]");
            }
            Console.WriteLine();
            string fileName = "array.json";
            using FileStream createStream = File.Create(fileName);
            JsonSerializer.Serialize(createStream, arr);
            createStream.Close();
            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<int>>(json);

        }
        static int PlinqMax(List<int> list)
        {
            var max = list.AsParallel()
                          .Max();
            return max;
        }
        static int PlinqMin(List<int> list)
        {
            var max = list.AsParallel()
                          .Min();
            return max;
        }
        static int PlinqSum(List<int> list)
        {
            var max = list.AsParallel()
                          .Sum();
            return max;
        }

        static void Main(string[] args)
        {
        //*Task 1-2
            int number = 5;
            Parallel.Invoke(() => Console.WriteLine($"The factorial is: {Factorial(number)}"),
                () => Console.WriteLine($"There are {FactorialCounter(number)} digits in factorial of {number}."),
                () => Console.WriteLine($"Suma of members of factorial of {number} is: {FactorialMembersSumma(number)}"));
        //*Task 3
            int startRange = 2;
            int endRange = 6;
            Parallel.For(startRange, endRange, MultiplicationTable);
            Random random = new Random();
            List<int> array = new List<int>();
            for (int i = 0; i <= 10; i++)
            {
                array.Add(random.Next(11));
            }
        //*Task 4
            Parallel.ForEach(array, FactorialVoid);

            List<int> list = SerializeDeserialize(array);
            Console.WriteLine($"Maximum value: {PlinqMax(list)}\n" +
                $"Minimum value: {PlinqMin(list)}\n" +
                $"Summa of values: {PlinqSum(list)}");

        }
    }
}
