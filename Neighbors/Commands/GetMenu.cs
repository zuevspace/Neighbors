using System.Reflection.Metadata;
using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Neighbors.Commands;

public class GetMenu
{
    [ReplyMenuHandler("Меню")]
    [SlashHandler("/start")]
    public static async Task ReplyMenu(ITelegramBotClient botClient, Update update)
    {
        var message = "Выберите действие:";
        var option = new OptionMessage();
        var menuList = new List<KeyboardButton>();
        
        menuList.Add(new KeyboardButton("Найти соседа"));
        menuList.Add(new KeyboardButton("Моя информация"));
        menuList.Add(new KeyboardButton("Статистика дома"));
        menuList.Add(new KeyboardButton("Полезная информация"));
        
        var menu = MenuGenerator.ReplyKeyboard(2, menuList);
        option.MenuReplyKeyboardMarkup = menu;
        
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
    }
    
    [ReplyMenuHandler("ADMIN_MENU")]
    public static async Task ReplyAdminMenu(ITelegramBotClient botClient, Update update)
    {
        var message = "";
        
        if (!botClient.IsAdmin(update.GetChatId()))
        {
            message = "Вы не являетесь админом!";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
        
        message = "Выберите действие:";
        var option = new OptionMessage();
        var menuList = new List<KeyboardButton>();
        
        menuList.Add(new KeyboardButton("Добавить соседа"));
        menuList.Add(new KeyboardButton("Редактировать соседа"));
        menuList.Add(new KeyboardButton("Удалить соседа"));
        
        var menu = MenuGenerator.ReplyKeyboard(2, menuList);
        option.MenuReplyKeyboardMarkup = menu;
        
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
    }
}