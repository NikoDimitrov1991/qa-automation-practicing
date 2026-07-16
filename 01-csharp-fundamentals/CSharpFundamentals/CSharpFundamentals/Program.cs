using CSharpFundamentals;

Console.WriteLine("Choose an exercise:");
Console.WriteLine("1) Even or Odd");
Console.WriteLine("2) Sum from 1 to N");
Console.WriteLine("3) FizzBuzz");
Console.WriteLine("4) OOP Demo (Kafka messages)");
Console.WriteLine("5) LINQ Demo (Kafka messages)");
Console.Write("Your choice: ");

string choice = Console.ReadLine();

switch (choice)
{
    case "1":
        EvenOdd.Run();
        break;
    case "2":
        SumToN.Run();
        break;
    case "3":
        FizzBuzz.Run();
        break;
    case "4":
        OopDemo.Run();
        break;
    case "5":
        LinqDemo.Run();
        break;
    default:
        Console.WriteLine("Invalid choice.");
        break;
}