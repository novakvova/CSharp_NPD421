namespace _1_Delegate;

public class Car
{
    public int CurrentSpeed { get; set; }
    public int MaxSpeed { get; set; } = 100;
    public string PetName { get; set; } = String.Empty;

    private bool carIdDead;

    //Консоль, у яку можна передавати повідомлення
    //Під консолю ми розуміємо - екрран у авто, може бути, індиктор на панелі прикладів

    //двигун може видавати повідомлення
    public delegate void CarEngineHandler(string message);

    public event CarEngineHandler? listOfHandlers; //Подія, яка буде викликатися, коли двигун авто видасть повідомлення



    /*
    //Подія, яка буде викликатися, коли двигун авто видасть повідомлення
    private CarEngineHandler? listOfHandlers;
    //Тут ми реєструємо вказіник на метод, який буде оброляти наша консоль.
    public void RegisterWithCarEngine(CarEngineHandler methodToCall)
    {
        //Додаємо метод до списку обробників подій
        listOfHandlers += methodToCall;
    }
    public void UnregisterWithCarEngine(CarEngineHandler methodToCall)
    {
        //Видаляємо метод зі списку обробників подій
        listOfHandlers -= methodToCall;
    }
    */

    public Car()
    {
        
    }
    public Car(string petName, int maxSpeed, int currentSpeed)
    {
        PetName = petName;
        MaxSpeed = maxSpeed;
        CurrentSpeed = currentSpeed;
    }

    public void Accelerate(int delta)
    {
        if (carIdDead)  //якщо авто поломилося, видаємо повідомлення на консоль
        {
            if(listOfHandlers != null)
            {
                listOfHandlers("Авто не працює, двигун зламаний");
                return;
            }
            //listOfHandlers?.Invoke("Авто не працює, двигун зламаний");
            //return;
        }
        CurrentSpeed += delta; //Якщо двигун не зломаний збільшуємо швидкість авто на delta
        if (CurrentSpeed >= MaxSpeed)  //Перевіряємо чи ми досягли маискльної швидкості
        {
            carIdDead = true;
            CurrentSpeed = 0;
            listOfHandlers?.Invoke($"Авто {PetName} зупинилося, двигун зламаний");
        }
        else
        {
            listOfHandlers?.Invoke($"Авто {PetName} прискорилося до {CurrentSpeed} км/год");
        }
    }

}
