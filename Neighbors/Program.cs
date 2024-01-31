using System.IO;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Neighbors;

class Program
{
    static async Task Main()
    {
        var path = @"/Users/zuev/SourceTree/Neighbors/Neighbors/TOKEN.txt";
        string token;
        using (var reader = new StreamReader(path)) { token = await reader.ReadToEndAsync(); }
        
        var botClient = new TelegramBotClient(token);
        var connString = "host=localhost;username=postgres;password=8888;database=postgres";
        var repository = new FlatRepository(connString);
        
        using CancellationTokenSource cts = new ();
        
        ReceiverOptions receiverOptions = new ()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };
        
        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );
        
        var me = await botClient.GetMeAsync();
        
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
        
        cts.Cancel();
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            switch (messageText)
            {
                case "/home_stat":
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Статистика дома: {repository.GetFlatCount()} квартир",
                        cancellationToken: cancellationToken);
                    break;
                case "/search":
                    //TODO: написать код который будет принимать от пользователя данные
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Напишите номер квартиры.",
                        cancellationToken: cancellationToken);

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
                default:
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Такой команды нет.",
                        cancellationToken: cancellationToken);
                    break;
            }
            
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        
        // var command = "";
        //
        // while (command != "/exit")
        // {
        //     Console.WriteLine("Ваша команда:");
        //     command = Console.ReadLine();
        //     
        //     switch (command)
        //     {
        //         case "/home_stat":
        //             Console.WriteLine($"Статистика дома: {repository.GetFlatCount()} квартир");
        //             break;
        //         case "/search":
        //             Console.WriteLine("Напишите номер квартиры.");
        //             var num = int.Parse(Console.ReadLine());
        //             var flat = repository.GetFlat(num);
        //
        //             if (flat != null)
        //             {
        //                 Console.WriteLine($"Номер квартиры: {flat.NumberFlat}");
        //                 Console.WriteLine($"Этаж: {flat.NumberFloors}");
        //                 Console.WriteLine($"Имя жильца: {flat.NameLodger}");
        //                 Console.WriteLine($"Номер телефона: {flat.PhoneNumber}");
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Квартира не найдена");
        //             }
        //             break;
        //         case "/add":
        //             Console.WriteLine("Добавить новую квартиру.");
        //             
        //             Console.WriteLine("Номер квартиры:");
        //             var flatNum = int.Parse(Console.ReadLine());
        //             Console.WriteLine("Номер этажа:");
        //             var floorsNum = int.Parse(Console.ReadLine());
        //             Console.WriteLine("Имя жильца:");
        //             var lodgerName= Console.ReadLine();
        //             Console.WriteLine("Номер телефона:");
        //             var phoneNum= Console.ReadLine();
        //             
        //             var newFlat = new Flat
        //             {
        //                 NumberFlat = flatNum,
        //                 NumberFloors = floorsNum,
        //                 NameLodger = lodgerName,
        //                 PhoneNumber = phoneNum
        //             };
        //             
        //             repository.AddFlat(newFlat);
        //             Console.WriteLine("Квартира добавлена успешно");
        //             
        //             break;
        //         case "/all":
        //             Console.WriteLine("Все жильцы.");
        //             
        //             break;
        //         default:
        //             Console.WriteLine("Такой команды нет.");
        //             
        //             break;
        //     }
        // }
    }
}