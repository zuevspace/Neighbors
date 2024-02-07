using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Xml;

namespace Neighbors;

public class Flat
{
    public int NumberFlat { get; set; }
    public int NumberFloors { get; set; }
    public string? NameLodger { get; set; }
    public string? PhoneNumber { get; set; }
    public string StringInfoFlat()
    {
        return $"Имя жильца: {NameLodger}\n" +
               $"Этаж: {NumberFloors}\n" +
               $"Номер квартиры: {NumberFlat}\n" +
               $"Номер телефона: {PhoneNumber}\n";
    }
}