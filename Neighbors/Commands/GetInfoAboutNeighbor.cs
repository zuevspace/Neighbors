using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Neighbors.Commands;

public class GetInfoAboutNeighbor
{
    [ReplyMenuHandler("/search")]
    public static async Task GetHouseStatistics2(ITelegramBotClient botClient, Update update)
    {
        var message = "Напишите номер квартиры:";
        update.RegisterStepHandler(new StepTelegram(StepNumFlat, new StepCache()));
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }
    
    /// <summary>
    /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
    /// Метод регистрирует следующий шаг с максимальным времени выполнения
    /// </summary>
    public static async Task StepNumFlat(ITelegramBotClient botClient, Update update)
    {
        var connString = "host=localhost;username=postgres;password=8888;database=postgres";
        var repository = new FlatRepository(connString);
        
        var num = int.Parse(update.Message.Text);
        var flat = repository.GetFlat(num);
        var msg = "";
        
        if (flat != null)
        {
            msg = $"Номер квартиры: {flat.NumberFlat}\n" +
                $"Этаж: {flat.NumberFloors}\n" +
                $"Имя жильца: {flat.NameLodger}\n" +
                $"Номер телефона: {flat.PhoneNumber}";
        }
        else
        {
            msg = "Квартира не найдена";
        }
        
        //Получаем текущий обработчик
        var handler = update.GetStepHandler<StepTelegram>();
        //Записываем имя пользователя в кэш 
        handler!.GetCache<StepCache>().Flat = update.Message.Text;
        //Последний шаг
        update.ClearStepUserHandler();
        await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
    }
}