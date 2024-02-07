using Neighbors;

namespace Neighbors;
public class FlatRepository
{
    private readonly string connectionString;

    public FlatRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public long GetFlatCount()
    {
        // using var connection = new NpgsqlConnection(connectionString);
        // connection.Open();
        //
        // using var command = new NpgsqlCommand("SELECT COUNT(1) FROM neighbors", connection);
        // var count = (long)command.ExecuteScalar();
        return 0;
    }
    public Flat? GetFlat(int flatNumber)
    {
        // using var connection = new NpgsqlConnection(connectionString);
        // connection.Open();
        //
        // using var command = new NpgsqlCommand("SELECT * FROM public.neighbors WHERE number_flat = @flatNumber", connection);
        // command.Parameters.AddWithValue("@flatNumber", flatNumber);
        //
        // using var reader = command.ExecuteReader();
        // if (reader.Read())
        // {
        //     return new Flat
        //     {
        //         NumberFlat = reader.GetInt32(reader.GetOrdinal("number_flat")),
        //         NumberFloors = reader.GetInt32(reader.GetOrdinal("number_floors")),
        //         NameLodger = reader.GetString(reader.GetOrdinal("name_lodger")),
        //         PhoneNumber = reader.GetString(reader.GetOrdinal("phone_number")),
        //     };
        // }

        return null;
    }

    public List<long> GetWhiteListUsers()
    {
        var list = new List<long>();
        
        // using var connection = new NpgsqlConnection(connectionString);
        // connection.Open();
        //
        // using var command = new NpgsqlCommand("SELECT id_telegram FROM public.neighbors UNION ALL SELECT id_telegram FROM public.add_neighbors;", connection);
        //
        // using var reader = command.ExecuteReader();
        // while (reader.Read())
        // {
        //     list.Add(reader.GetInt64(reader.GetOrdinal("id_telegram")));
        // }
        
        return list;
    }
    
    public void AddFlat(Flat flat)
    {
        // using var connection = new NpgsqlConnection(connectionString);
        // connection.Open();
        //
        // using var command = new NpgsqlCommand("INSERT INTO public.neighbors VALUES (@number_flat, @number_floors, @name_lodger, @phone_number)", connection);
        // command.Parameters.AddWithValue("@number_flat", flat.NumberFlat);
        // command.Parameters.AddWithValue("@number_floors", flat.NumberFloors);
        // command.Parameters.AddWithValue("@name_lodger", flat.NameLodger);
        // command.Parameters.AddWithValue("@phone_number", flat.PhoneNumber);
        //
        // command.ExecuteNonQuery();
    }

}