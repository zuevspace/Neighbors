using System;
using Neighbors;
using Npgsql;

public class FlatRepository
{
    private readonly string connectionString;

    public FlatRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public long GetFlatCount()
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        using var command = new NpgsqlCommand("SELECT COUNT(1) FROM public.neighbors", connection);
        var count = (long)command.ExecuteScalar();

        return count;
    }
    public Flat? GetFlat(int flatNumber)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT * FROM public.neighbors WHERE number_flat = @flatNumber", connection);
        command.Parameters.AddWithValue("@flatNumber", flatNumber);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Flat
            {
                NumberFlat = reader.GetInt32(reader.GetOrdinal("number_flat")),
                NumberFloors = reader.GetInt32(reader.GetOrdinal("number_floors")),
                NameLodger = reader.GetString(reader.GetOrdinal("name_lodger")),
                PhoneNumber = reader.GetString(reader.GetOrdinal("phone_number"))
            };
        }

        return null;
    }
    
    public void AddFlat(Flat flat)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("INSERT INTO public.neighbors VALUES (@number_flat, @number_floors, @name_lodger, @phone_number)", connection);
        command.Parameters.AddWithValue("@number_flat", flat.NumberFlat);
        command.Parameters.AddWithValue("@number_floors", flat.NumberFloors);
        command.Parameters.AddWithValue("@name_lodger", flat.NameLodger);
        command.Parameters.AddWithValue("@phone_number", flat.PhoneNumber);

        command.ExecuteNonQuery();
    }

}