namespace CSharpFundamentals;

public static class SumToN
{
    public static void Run()
    {
        Console.WriteLine("Enter a number n:");
        string input = Console.ReadLine();
        int n = int.Parse(input);
        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i;
        }
        Console.WriteLine($"The sum from 1 to {n} is: {sum}");
    }
}