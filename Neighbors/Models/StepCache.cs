using PRTelegramBot.Interface;

namespace Neighbors;

public class StepCache : ITelegramCache
{
    public string Flat { get; set; }
    public bool ClearData()
    {
        this.Flat = string.Empty;
        return true;
    }
}