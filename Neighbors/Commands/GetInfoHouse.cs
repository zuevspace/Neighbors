using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Neighbors.Commands;

public class GetInfoHouse
{
    [ReplyMenuHandler("Информация","инфа")]
    [SlashHandler("/info")]
    public static async Task ReplyInfoHouse(ITelegramBotClient botClient, Update update)
    {
        var message = $"Правила поведения в доме:\nЖители дома «3.4.1» обязаны соблюдать следующие правила:\n— Не шуметь после 22:00.\n— Соблюдать чистоту в общественных местах.\n— Не оставлять мусор в подъезде.\n— Следить за сохранностью имущества общего пользования.\n— Не разбрасывать окурки на территории около дома.\nКонтакты экстренных служб:\nПолиция — 102.\nСкорая помощь — 112.\nПожарная охрана — 101.\nАварийная газовая служба — 104.";
        await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
    }
}