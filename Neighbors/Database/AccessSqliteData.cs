using Dapper;
using System.Data;
using System.Data.SQLite;
using Telegram.Bot.Types;

namespace Neighbors.Database;

public class AccessSqliteData
{
    private static string LoadConnectionString()
    {
        var path = Path.Combine("Database", "neighbors.sqlite");
        return $"Data Source={path};Version=3";
    }

    public static Flat? SearchMyInfo(long id)
    {
        using var connection = new SQLiteConnection(LoadConnectionString());
        connection.OpenAsync();
        var sql =
            "SELECT number_flat, number_floors, name_lodger, phone_number FROM neighbors WHERE id_telegram = @id";
        var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            return new Flat
            {
                NumberFlat = reader.GetInt32(0),
                NumberFloors = reader.GetInt32(1),
                NameLodger = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };
        }
        connection.CloseAsync();
        return null;
    }
    
    public static List<Flat> LoadFlatAsync()
    {
        using var connection = new SQLiteConnection(LoadConnectionString());
        connection.OpenAsync();
        var command = new SQLiteCommand("SELECT number_flat, number_floors, name_lodger, phone_number FROM neighbors",
            connection);

        var flats = new List<Flat>();
        
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            var flat = new Flat
            {
                NumberFlat = reader.GetInt32(0),
                NumberFloors = reader.GetInt32(1),
                NameLodger = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };
            flats.Add(flat);
        }
        connection.CloseAsync();
        
        return flats;
    }

    public static Flat? SearchFlatAsync(int flatNumber)
    {
        using var connection = new SQLiteConnection(LoadConnectionString());
        connection.OpenAsync();
        var sql =
            "SELECT number_flat, number_floors, name_lodger, phone_number FROM neighbors WHERE number_flat = @number_flat";
        var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@number_flat", flatNumber);
        var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            return new Flat
            {
                NumberFlat = reader.GetInt32(0),
                NumberFloors = reader.GetInt32(1),
                NameLodger = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };
        }
        connection.CloseAsync();
        return null;
    }
    
    public static void InsertFlatAsync(long id, string name)
    {
        using var connection = new SQLiteConnection(LoadConnectionString());
        connection.OpenAsync();
        const string sql = "INSERT INTO neighbors(id_telegram, name_lodger) VALUES (@id_telegram, @name_lodger)";
        var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id_telegram", id);
        command.Parameters.AddWithValue("@name_lodger", name);
        command.ExecuteNonQuery();
    }
}