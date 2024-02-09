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

        if (myFlat != null)
        {
            var message = myFlat.StringInfoFlat();
            await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
        else
        {
            var chat = update.Message.Chat;
            AccessSqliteData.InsertFlatAsync(chat.Id, chat.Username);
            
            // var message = "Вы не найдены в базе, пройдите регистрацию.\nНапишите ваш телефон:"
            // update.RegisterStepHandler(new StepTelegram(StepPhone, new StepCache()));
            // await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
    }
    
    // private static async Task StepPhone(ITelegramBotClient botClient, Update update)
    // {
    //     var pars = int.TryParse(update.Message.Text, out var num);
    //
    //     
    //     //Получаем текущий обработчик
    //     var handler = update.GetStepHandler<StepTelegram>();
    //     //Записываем имя пользователя в кэш 
    //     handler!.GetCache<StepCache>().Flat = update.Message.Text;
    //     //Последний шаг
    //     update.ClearStepUserHandler();
    //     await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
    // }
}