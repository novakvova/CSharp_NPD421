// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;
using Bogus;
using Newtonsoft.Json;
using static Bogus.DataSets.Name;

namespace _3_AnonimObject;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int action = 0;
        var users = new List<User>();
        string temp = String.Empty;
        do
        {
            Console.WriteLine("Виберіть дію:");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("1. Згенерувати користувачів");
            Console.WriteLine("2. Вивести користувачів на екран");
            Console.WriteLine("3. Зчитати користувачів з файлу");
            Console.WriteLine("4. Записати користувачів у файл");
            Console.Write("Ваш вибір: ");
            action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    Console.Write("Скільки користувачів згенерувати? ");
                    int count = int.Parse(Console.ReadLine());
                    users = GetRandomUsers(count).ToList();
                    Console.WriteLine($"Згенеровано {users.Count} користувачів.");
                    break;
                case 2:
                    PrintUsers(users);
                    break;
                case 3:
                    try
                    {
                        Console.WriteLine("Вкажіть назву файлу");
                        temp = Console.ReadLine() ?? "";
                        users = GetUsersFromFile(temp).ToList();
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    Console.WriteLine("Вкажіть назву файлу");
                    string myFileName = Console.ReadLine() ?? "";
                    var json = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText(temp, json, Encoding.UTF8);
                    Console.WriteLine($"Користувачі записані у файл {temp}");
                    break;
                case 0:
                    Console.WriteLine("Вихід з програми.");
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        } while (action!=0);
    }

    public static IEnumerable<User> GetRandomUsers(int count)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var userIds = 1;
        var myUserFaker = new Faker<User>("uk")
            .CustomInstantiator(f => new User(userIds++))
            .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Avatar, f => f.Internet.Avatar()); // Генеруємо аватар користувача

        var users = myUserFaker.Generate(count);

        stopWatch.Stop();

        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);

        return users;
    }


    /// <summary>
    /// Зчитує користувачів з файлу у форматі JSON.
    /// </summary>
    /// <param name="fileName">Передаємо назву файлу</param>
    /// <returns>Повертаємо у масиві список користувачів</returns>
    /// <exception cref="FileNotFoundException">Може вкидати виключення, якщо файлу немає</exception>
    public static IEnumerable<User> GetUsersFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"Файл {fileName} не знайдено.");
        }
        var json = File.ReadAllText(fileName, Encoding.UTF8);
        var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json) ?? Enumerable.Empty<User>();
        return users;
    }

    public static void PrintUsers(IEnumerable<User> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }
}




