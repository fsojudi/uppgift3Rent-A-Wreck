namespace RentAWreck.Domain;

class Vehicle
{
    public required string Brand { get; set; }

    public required string Model { get; set; }

    public required VehicleType Type { get; set; }

    public required string RegistrationNumber { get; set; }
}