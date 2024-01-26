using System;
using Neighbors;
using Npgsql;

class Program
{
    static void Main()
    {
        var connString = "host=localhost;username=postgres;password=8888;database=postgres";
        var repository = new FlatRepository(connString);
        
        var command = "";
        
        while (command != "/exit")
        {
            Console.WriteLine("Ваша команда:");
            command = Console.ReadLine();
            
            switch (command)
            {
                case "/home_stat":
                    Console.WriteLine($"Статистика дома: {repository.GetFlatCount()} квартир");
                    break;
                case "/search":
                    Console.WriteLine("Напишите номер квартиры.");
                    var num = int.Parse(Console.ReadLine());
                    var flat = repository.GetFlat(num);

                    if (flat != null)
                    {
                        Console.WriteLine($"Номер квартиры: {flat.NumberFlat}");
                        Console.WriteLine($"Этаж: {flat.NumberFloors}");
                        Console.WriteLine($"Имя жильца: {flat.NameLodger}");
                        Console.WriteLine($"Номер телефона: {flat.PhoneNumber}");
                    }
                    else
                    {
                        Console.WriteLine("Квартира не найдена");
                    }
                    
                    break;
                case "/add":
                    Console.WriteLine("Добавить новую квартиру.");
                    
                    Console.WriteLine("Номер квартиры:");
                    var flatNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Номер этажа:");
                    var floorsNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Имя жильца:");
                    var lodgerName= Console.ReadLine();
                    Console.WriteLine("Номер телефона:");
                    var phoneNum= Console.ReadLine();
                    
                    var newFlat = new Flat
                    {
                        NumberFlat = flatNum,
                        NumberFloors = floorsNum,
                        NameLodger = lodgerName,
                        PhoneNumber = phoneNum
                    };
                    
                    repository.AddFlat(newFlat);
                    Console.WriteLine("Квартира добавлена успешно");
                    
                    break;
                case "/all":
                    Console.WriteLine("Все жильцы.");
                    
                    break;
                default:
                    Console.WriteLine("Такой команды нет.");
                    
                    break;
            }
        }
    }
}