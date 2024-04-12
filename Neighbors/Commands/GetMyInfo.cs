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
        var myFlat = AccessSqliteData.SearchMyInfo(update.Message.Chat.Id);
        var message = myFlat.GetInfoAboutFlat();
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }
}