namespace CSharpFundamentals;

public static class EvenOdd
{
    public static void Run()
    {
        Console.WriteLine("Enter a number:");
        string input = Console.ReadLine();
        int number = int.Parse(input);

        if (number % 2 == 0)
        {
            Console.WriteLine($"{number} is Even.");
        }
        else
        {
            Console.WriteLine($"{number} is Odd.");
        }
    }
}