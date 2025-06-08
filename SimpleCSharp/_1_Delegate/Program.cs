namespace _1_Delegate;

/// Делегат - це вказівник на метод. 
/// Він дозволяє зберігати посилання на методи з певним сигнатурою.
/// Метод має приймати два парамтера типу інт і повертати значення типу інт.
delegate int OperationDelegate(int a, int b);
//delegate int OperationDelegate(int a, int b);

class MathOperations
{
    // Метод, який відповідає сигнатурі делегата
    static public int Add(int a, int b) => a + b;
     
    // Метод, який відповідає сигнатурі делегата
    static public int Subtract(int a, int b)
    {
        return a - b;
    }
}

// Делегат, який приймає рядок і не повертає значення
delegate void DisplayDelegate(string message);
class Program
{
    static void DisplayDanger(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    static void DisplaySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        /*
        // Створюємо екземпляр делегата, який вказує на метод Add
        OperationDelegate operationDelegate;
        //Делегат може вказувати на метод Add
        //operationDelegate = new OperationDelegate(MathOperations.Add);
        operationDelegate = new OperationDelegate(MathOperations.Subtract);

        var result = operationDelegate(10, 5); // Викликаємо метод Add через делегат

        Console.WriteLine($"Результат роботи метода {result}");

        */

        Console.WriteLine("Вкажіть ваш вік для водіння авто");
        int age = int.Parse(Console.ReadLine() ?? "0");

        DisplayDelegate displayDelegate;
        string message;
        if (age >= 18)
        {
            displayDelegate = DisplaySuccess;
            message = "Вітаємо! Ви можете керувати автомобілем.";
        }
        else
        {
            displayDelegate = DisplayDanger;
            message = "На жаль, ви ще не досягли віку для керування автомобілем.";
        }

        displayDelegate(message);
    }
}








