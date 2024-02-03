using PRTelegramBot.Core;

class Program
{
    static async Task Main()
    {
        var path = @"/Users/zuev/SourceTree/Neighbors/Neighbors/TOKEN.txt";
        string token;
        using (var reader = new StreamReader(path))
        {
            token = await reader.ReadToEndAsync();
        }
        
        var telegram = new PRBot(options =>
        {
            options.Token = token;
            options.ClearUpdatesOnStart = true;
            // Если есть хоть 1 идентификатор телеграм пользователя, могут пользоваться только эти пользователи
            options.WhiteListUsers = new List<long>() { };
            // Идентификатор администраторов бота
            options.Admins = new List<long>() { };
            options.BotId = 0;
        });
 
        //Подписка на простые логи
        telegram.OnLogCommon += TelegramOnLogCommon;
        //Подписка на логи с ошибками
        telegram.OnLogError += TelegramOnLogError;
        
        await telegram.Start();
        
        void TelegramOnLogError(Exception ex, long? id = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var errorMsg = $"{DateTime.Now}: {ex}";
            Console.WriteLine(errorMsg);
            Console.ResetColor();
        }
        
        void TelegramOnLogCommon(string msg, Enum typeEvent, ConsoleColor color = ConsoleColor.Blue)
        {
            Console.ForegroundColor = color;
            var formatMsg = $"{DateTime.Now}: {msg}";
            Console.WriteLine(formatMsg);
            Console.ResetColor();
        }

        while (true)
        {
            var exit = Console.ReadLine();
            if (exit == "EXIT_COMMAND")
            {
                Environment.Exit(0);
            }
        }
    }
}
