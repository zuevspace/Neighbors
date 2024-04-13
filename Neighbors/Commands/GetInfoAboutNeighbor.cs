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

public class GetInfoAboutNeighbor
{
    [ReplyMenuHandler("Найти соседа","Найти","Поиск","Квартира")]
    [SlashHandler("/search")]
    public static async Task GetHouseStatistics(ITelegramBotClient botClient, Update update)
    {
        var message = "Напишите номер квартиры:";
        update.RegisterStepHandler(new StepTelegram(StepNumFlat, new StepCache()));
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }

    private static async Task StepNumFlat(ITelegramBotClient botClient, Update update)
    {
        var pars = int.TryParse(update.Message?.Text, out var num);

        var flat = await AccessSqliteData.SearchFlatAsync(num);

        if (!pars || flat == null)
            await PRTelegramBot.Helpers.Message.Send(botClient, update, "Квартира не найдена");
        
        var msg = flat?.GetInfoAboutFlat();
        
        var write = new InlineURL("Написать", $"tg://resolve?phone={flat?.PhoneNumber}");
        
        var menu = new List<IInlineContent> { write };

        var writeMenu = MenuGenerator.InlineKeyboard(1, menu);
        
        var option = new OptionMessage
        {
            MenuInlineKeyboardMarkup = writeMenu
        };

        //Получаем текущий обработчик
        var handler = update.GetStepHandler<StepTelegram>();
        //Записываем имя пользователя в кэш 
        handler!.GetCache<StepCache>().Flat = update.Message.Text;
        //Последний шаг
        update.ClearStepUserHandler();
        await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
    }
}