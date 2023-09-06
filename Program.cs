
using Microsoft.Data.SqlClient;
using RentAWreck.Domain;
using static System.Console;

namespace RentAWreck;



class program
{
    static string connectionString = " Server=.; Database=RentAWreck;Integrated Security=true; Encrypt=False";
    static void Main()
    {
        CursorVisible = false;
        Title = "Rent-A-Wreck";

        while (true)
        {
            WriteLine("1. Registrera fordon");
            WriteLine("2. Lista fordon");
            WriteLine("3. Sök fordon");
            WriteLine("4. Avsluta");

            var keyPressed = ReadKey(intercept: true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    RegisterVehicle();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    ListVehicles();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    SearchVehicle();

                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }

    }




    
    private static void RegisterVehicle()
    {
        Write("Märke: ");

        string brand = ReadLine();

        Write("Model: ");

        string model = ReadLine();

        Write("Typ: ");

        VehicleType type = Enum.Parse<VehicleType>(ReadLine(), true);

        Write("Registreringsnummer: ");

        string registrationNumber = ReadLine();

        Clear();

        var vehicle = new Vehicle
        {
            Brand = brand,
            Model = model,
            Type = type,
            RegistrationNumber = registrationNumber
        };

        SaveVehicle(vehicle);

        WriteLine("Fordon registrerat");

        Thread.Sleep(2000);
    }

    private static void SaveVehicle(Vehicle vehicle)
    {
        //INSERT INTO
        string sql = @"
        INSERT INTO Vehicle(
        Brand, 
        Model, 
        Type,
        RegistrationNumber)
        VALUES(
        @Brand,
        @Model,
        @Type,
        @RegistrationNumber)";

        using var connection = new SqlConnection(connectionString);

        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Brand", vehicle.Brand);
        command.Parameters.AddWithValue("@Model", vehicle.Model);
        command.Parameters.AddWithValue("@Type", (int) vehicle.Type);
        command.Parameters.AddWithValue("@RegistrationNumber",  vehicle.RegistrationNumber);

        connection.Open();

        command.ExecuteNonQuery();

        connection.Close(); 



    }

    private static void SearchVehicle()
    {
        Write("Registreringsnummer: ");

        string registrationNumber = ReadLine();

        Clear();

        var vehicle = SearchVehicle(registrationNumber);
         
        if (vehicle is not null)
        {
            WriteLine($"Märke: {vehicle.Brand}");
            WriteLine($"Model: {vehicle.Model}");
            WriteLine($"Typ: {vehicle.Type}");
            WriteLine($"RegistreringsNummer: {vehicle.RegistrationNumber}");

            WaitUntilKeyPressed(ConsoleKey.Escape);
        }
        else
        {
            WriteLine("Fordon finns ej");

            Thread.Sleep(2000);
        }
    }

    private static Vehicle? SearchVehicle(string registrationNumber)
    {

        var sql = @"
            SELECT Brand ,
                   Model,
                   type,
                   RegistrationNumber
            FROM Vehicle  
            WHERE RegistrationNumber= @RegistrationNumber
";
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);

        connection.Open();

        var reader = command.ExecuteReader();

        Vehicle vehicle = null;
        if (reader.Read())
        {
            vehicle = new Vehicle 
            {

                Brand = reader["Brand"].ToString(),
                Model = reader["Model"].ToString(),
                Type = (VehicleType)reader["Type"],
                RegistrationNumber = reader["RegistrationNumber"].ToString(),

            };
        }

        connection.Close();
        return vehicle;
    }

    private static void ListVehicles()
    {
        Write($"{"Märke",-10}");
        Write($"{"Modell",-10}");
        Write($"{"Typ",-10}");
        WriteLine("Registreringsnummer");

        WriteLine(new string('-', 60));

        var vehicles = FetchVehicle();

        foreach (var vehicle in vehicles)
        {
            Write($"{vehicle.Brand,-10}");
            Write($"{vehicle.Model,-10}");
            Write($"{vehicle.Type,-10}");
            WriteLine(vehicle.RegistrationNumber);
        }

        WaitUntilKeyPressed(ConsoleKey.Escape);
    }

    private static IEnumerable<Vehicle> FetchVehicle()
    {
        var vehicles = new List<Vehicle>();

        var sql = @"
            SELECT Brand ,
                   Model,
                   type,
                   RegistrationNumber
            FROM Vehicle  
";
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();

        var reader= command.ExecuteReader();

        while (reader.Read())
        {
            var vehicle = new Vehicle()
            {

                Brand = reader["Brand"].ToString(),
                Model = reader["Model"].ToString(),
                Type = (VehicleType) reader["Type"],
                RegistrationNumber = reader["RegistrationNumber"].ToString(),

            };
            vehicles.Add(vehicle);

        }
        connection.Close();
        return vehicles;
    }

    private static void WaitUntilKeyPressed(ConsoleKey key)
    {
        while (ReadKey(true).Key != ConsoleKey.Escape) ;
    }



}
