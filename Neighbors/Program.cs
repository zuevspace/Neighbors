using Neighbors.Database;
using PRTelegramBot.Core;

namespace Neighbors;
class Program
{
    static async Task Main()
    {
        var telegram = new PRBot(options =>
        {
            options.Token = GetTokenBot.Get().Result;
            options.ClearUpdatesOnStart = true;
            options.WhiteListUsers = new List<long>() { 132493648, 663256732, 1417023281};
            options.Admins = new List<long>() { 132493648, 1417023281 };
            options.BotId = 0;
        });
        
        telegram.OnLogCommon += TelegramOnLogCommon;
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
