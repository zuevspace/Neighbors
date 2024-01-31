namespace Neighbors;

public class Flat
{
    public int NumberFlat { get; init; } //Квартира
    public int NumberFloors { get; init; } //Этаж
    public string? NameLodger { get; init; } //Имя жильца
    public string? PhoneNumber { get; init; } //Номер телефона
    public void PrintInfo()
    {
        Console.WriteLine($"Номер квартиры: {NumberFlat}\n" +
                          $"Этаж: {NumberFloors}\n" +
                          $"Имя жильца: {NameLodger}\n" +
                          $"Номер телефона: {PhoneNumber}\n");
    }
}