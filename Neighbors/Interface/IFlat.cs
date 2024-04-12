namespace Neighbors.Interface;

public interface IFlat
{
    public int NumberFlat { get;}
    public int NumberFloors { get; set; }
    public string? NameLodger { get; set; }
    public string? PhoneNumber { get; set; }
    public int NumberSection { get; set; }
    public string GetInfoAboutFlat();
}