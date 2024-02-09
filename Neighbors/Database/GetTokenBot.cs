namespace Neighbors.Database;

public class GetTokenBot
{
    public static async Task<string> Get()
    {
        var path = Path.Combine("TOKEN.txt");
        using var reader = new StreamReader(path);
        var token = await reader.ReadToEndAsync();
        return token;
    }
}