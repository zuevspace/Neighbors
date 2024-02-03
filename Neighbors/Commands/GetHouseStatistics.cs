using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Neighbors.Commands;

public class GetHouseStatistics
{
    [ReplyMenuHandler("/stat")]
    public static async Task GetHouseStatistics2(ITelegramBotClient botClient, Update update)
    {
        var connString = "host=localhost;username=postgres;password=8888;database=postgres";
        var repository = new FlatRepository(connString);
        
        var message = $"Статистика дома: {repository.GetFlatCount()} квартир";
        var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }
}