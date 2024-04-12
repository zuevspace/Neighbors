using Neighbors.Interface;

namespace Neighbors;

public class Flat : IFlat
{
    public int NumberFlat { get; init; }
    public int NumberFloors { get; set; }
    public string? NameLodger { get; set; }
    public string? PhoneNumber { get; set; }
    public int NumberSection { get; set; }
    public string GetInfoAboutFlat()
    {
        return $"Имя: {NameLodger}\n" +
               $"Подъезд: {NumberSection}\n" +
               $"Этаж: {NumberFloors}\n" +
               $"Номер квартиры: {NumberFlat}\n" +
               $"Номер телефона: {PhoneNumber}\n";
    }
}