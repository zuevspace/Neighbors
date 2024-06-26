using Dapper;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Dapper;

namespace Neighbors.Database;

public class AccessSqliteData
{
    private static string LoadConnectionString()
    {
        var path = Path.Combine("Database", "neighbors.sqlite");
        return $"Data Source={path};Version=3";
    }

    public static async Task<Flat?> SearchMyInfoAsync(long id)
    {
        await using var connection = new SQLiteConnection(LoadConnectionString());
        const string selectQuery = $@"SELECT number_flat as {nameof(Flat.NumberFlat)}, number_floors as {nameof(Flat.NumberFloors)}, name_lodger as {nameof(Flat.NameLodger)}, phone_number as {nameof(Flat.PhoneNumber)} FROM neighbors WHERE id_telegram = @Id";
        IEnumerable<Flat?> result = await connection.QueryAsync<Flat>(selectQuery, new {Id = id});
        return result.FirstOrDefault();
    }
    public static async Task<Flat?> SearchFlatAsync(int flatNumber)
    {
        await using var connection = new SQLiteConnection(LoadConnectionString());
        const string selectQuery = $@"SELECT number_flat as {nameof(Flat.NumberFlat)}, number_floors as {nameof(Flat.NumberFloors)}, name_lodger as {nameof(Flat.NameLodger)}, phone_number as {nameof(Flat.PhoneNumber)} FROM neighbors WHERE number_flat = @NumberFlat";
        IEnumerable<Flat?> result = await connection.QueryAsync<Flat>(selectQuery, new {NumberFlat = flatNumber});
        return result.FirstOrDefault();
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