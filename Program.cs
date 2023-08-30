using static System.Console;


CursorVisible = false;
bool isrunnig = true;
Title = "Menu";


var carsList = new List<Car>();


do
{
    WriteLine("1 . Registera fordon");
    WriteLine("2 . Lista fordon");
    WriteLine("3 . Avsluta");

    ConsoleKeyInfo keypressed = ReadKey(intercept: true);
    Clear();

    switch (keypressed.Key)
    {
        case ConsoleKey.D1:
        case ConsoleKey.NumPad1:

            Write("Märke: " ); 
            string newBrand = ReadLine();
           

            Write("Modell: " );
            string newModel=ReadLine();
  
            Write("Typ: "); 
            string stringType=ReadLine();
            Type enumType;
             bool isCorrect = true;
            while (isCorrect=false)
            {
                if (Enum.TryParse(stringType, out Type newType))
                {
                    enumType = newType;
                }
                else
                {
                    WriteLine("ejgiltig");
                    isCorrect = false;
                }

            }
            Write("RegistrationNumber: ");
            string newRegistrationNumber=ReadLine();

            Car newCar = new Car(
                brand: newBrand,
                model: newModel,
                type: newType,
                registrationNumber: newRegistrationNumber
                );

            carsList.Add(newCar);

            ReadKey();
            Clear();
            break;

        case ConsoleKey.D2:
        case ConsoleKey.NumPad2:

            WriteLine("Märke" + "      " +
                "Modell" + "      " +
                 "Typ" + "      " +
                 "RegistrationNumber" + "      ");

            for (global::System.Int32 i = 0; i < 60; i++)
            {
                Write("-");
            }

            WriteLine("\n ");

            foreach (Car car in carsList)
            {  
                WriteLine(car.brand+"      "+
                  car.model + "      "+
                  car.type + "      "+
                  car.registrationNumber + "      ");
            }

            ReadKey();
            Clear();

            break;

        case ConsoleKey.D3:
        case ConsoleKey.NumPad3:

            Environment.Exit(0);

            return;
    }

    Clear();

} while (isrunnig);
public enum Type
{
    suv,
    sedan,
    kombi
}
class Car
{
    public string brand;
    public string model;
    public Type type;
    public string registrationNumber;




    public Car(string brand, string model, Type type, string registrationNumber)
    {
        this.brand = brand;
        this.model = model;
        this.type = type;
        this.registrationNumber = registrationNumber;
    }
}