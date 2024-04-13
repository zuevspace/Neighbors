using System.Text;
using Neighbors.Database;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interface;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Neighbors.Commands;

public class GetMyInfo
{
    [ReplyMenuHandler("Мои данные")]
    [SlashHandler("/my_info")]
    public static async Task ReplyMyInformation(ITelegramBotClient botClient, Update update)
    {
        var myFlat = await AccessSqliteData.SearchMyInfoAsync(update.Message.Chat.Id);
        var message = myFlat?.GetInfoAboutFlat();
        if (message == null)
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Ваших данных нет в базе.");
            
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }
}