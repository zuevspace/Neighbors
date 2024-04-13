using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Neighbors.Commands;

public class GetMenu
{
    const string menuTextSelectAction = "Выберите действие:";
    
    [ReplyMenuHandler("Меню")]
    [SlashHandler("/start")]
    public static async Task ReplyMenu(ITelegramBotClient botClient, Update update)
    {
        var option = new OptionMessage();
        var menu = new List<KeyboardButton>
        {
            new KeyboardButton("Найти соседа"),
            new KeyboardButton("Мои данные"),
            new KeyboardButton("Чат дома"),
            new KeyboardButton("Информация")
        };

        var mainMenu = MenuGenerator.ReplyKeyboard(2, menu);
        option.MenuReplyKeyboardMarkup = mainMenu;
        
        await PRTelegramBot.Helpers.Message.Send(botClient, update, menuTextSelectAction, option);
    }
    
    [ReplyMenuHandler("/admin")]
    public static async Task ReplyAdminMenu(ITelegramBotClient botClient, Update update)
    {
        if (!botClient.IsAdmin(update.GetChatId()))
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Вы не являетесь админом!");
        }
        
        var option = new OptionMessage();
        var menu = new List<KeyboardButton>
        {
            new KeyboardButton("Добавить соседа"),
            new KeyboardButton("Редактировать соседа"),
            new KeyboardButton("Удалить соседа")
        };

        var adminMenu = MenuGenerator.ReplyKeyboard(2, menu);
        option.MenuReplyKeyboardMarkup = adminMenu;
        
        await PRTelegramBot.Helpers.Message.Send(botClient, update, menuTextSelectAction, option);
    }
}