namespace Neighbors.Database;

public class GetTokenBot
{
    public static async Task<string> GetAsync()
    {
        using var reader = new StreamReader(Path.Combine("TOKEN.txt"));
        return await reader.ReadToEndAsync();
    }
}