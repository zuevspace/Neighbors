using Neighbors.Database;
using PRTelegramBot.Attributes;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interface;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Neighbors.Commands;

public class GetChatHouse
{
    [ReplyMenuHandler("Чат дома","чат")]
    [SlashHandler("/chat")]
    public static async Task ReplyChatHouse(ITelegramBotClient botClient, Update update)
    {
        var urlChat = new InlineURL("Чат дома", "https://t.me/buninskieluga341");
        
        var menu = new List<IInlineContent> { urlChat };

        var chatMenu = MenuGenerator.InlineKeyboard(1, menu);
        
        var option = new OptionMessage
        {
            MenuInlineKeyboardMarkup = chatMenu
        };
        var message = $"В чате дома можно обмениваться полезной информацией, обсуждать бытовые вопросы и помогать друг другу.\nЧтобы перейти в чат дома, нажмите на кнопку «Чат дома»";
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
    }
}