using Neighbors.Database;
using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Neighbors.Commands;

public class GetHouseStatistics
{
    [ReplyMenuHandler("Статистика дома","статистика")]
    [SlashHandler("/stat")]
    public static async Task ReplyHouseStatistics(ITelegramBotClient botClient, Update update)
    {
        var countFlat = AccessSqliteData.LoadFlatAsync().Count;
        
        var message = $"Статистика дома: {countFlat} квартир";
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }
}