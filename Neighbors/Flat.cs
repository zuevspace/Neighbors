namespace Neighbors;

public class Flat
{
    public int NumberFlat { get; set; } //Квартира
    public int NumberFloors { get; set; } //Этаж
    public string? NameLodger { get; set; } //Имя жильца
    public string? PhoneNumber { get; set; } //Номер телефона

    // public Flat(int numFlat, int numFloors, string nameLodger, string phoneNumber)
    // {
    //     NumberFlat = numFlat;
    //     NumberFloors = numFloors;
    //     NameLodger = nameLodger;
    //     PhoneNumber = phoneNumber;
    // }
    
    public void PrintInfo()
    {
        Console.WriteLine($"Номер квартиры: {NumberFlat}\n" +
                          $"Этаж: {NumberFloors}\n" +
                          $"Имя жильца: {NameLodger}\n" +
                          $"Номер телефона: {PhoneNumber}\n");
    }
}