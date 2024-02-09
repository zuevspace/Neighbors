using Dapper;
using System.Data;
using System.Data.SQLite;

namespace Neighbors.Database;

public class AccessSqliteData
{
    public static List<Flat> LoadFlat()
    {
        using var connection = new SQLiteConnection(LoadConnectionString());
        connection.Open();
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
        connection.Close();
        
        return flats;
    }

    public static Flat? SearchFlat(int flatNumber)
    {
        using var connection = new SQLiteConnection(LoadConnectionString());
        connection.Open();
        var sql =
            "SELECT number_flat, number_floors, name_lodger, phone_number FROM neighbors WHERE number_flat = @flatNumber";
        var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@flatNumber", flatNumber);
        var reader = command.ExecuteReader();
        
        if (reader.HasRows)
        {
            reader.Read();
            return new Flat
            {
                NumberFlat = reader.GetInt32(0),
                NumberFloors = reader.GetInt32(1),
                NameLodger = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };
        }
        else
        {
            return null;
        }
    }
    
    public static void InsertFlat(Flat flat)
    {
        using var con = new SQLiteConnection(LoadConnectionString());
        con.Execute(
            "INSERT INTO neighbors(number_flat, number_floors, name_lodger, phone_number, id_telegram) VALUES (@number_flat, @number_floors, @name_lodger, @phone_number, @id_telegram)", flat);
    }

    private static string LoadConnectionString()
    {
        var path = Path.Combine("Database", "neighbors.sqlite");
        return $"Data Source={path};Version=3";
    } 
}