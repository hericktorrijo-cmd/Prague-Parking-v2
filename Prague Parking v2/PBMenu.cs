using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using PragueParking.Data;

namespace Prague_Parking_v2
{
    public class Menu
    {

        private ParkingGarage _garage;
        private FileManager _fileManager;

        private PriceList _priceList;

        public Menu()
        {
            _fileManager = new FileManager();

            if (File.Exists("garage.json"))
            {
                _garage = _fileManager.Load();
            }
            else
            {
                Config config = _fileManager.LoadConfig();
                _garage = new ParkingGarage(config.NumberOfSpots);
            }
            _priceList = _fileManager.LoadPrices();
        }
        // Meny
        public void Start()
        {
            while(true)
            {
                Console.WriteLine();
                AnsiConsole.Write(
                    new Rule("[deeppink1 bold]Prague Parking[/]"));
                Console.WriteLine();

                AnsiConsole.MarkupLine("[grey]Parking Management System[/]");

                AnsiConsole.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Välj alternativ")
                    .AddChoices(
                        "Parkera fordon",
                        "Ta bort fordon",
                        "Sök fordon",
                        "Visa P-hus",
                        "Spara",
                        "Avsluta"));

                switch(choice)
                {
                    case "Parkera fordon":
                        ParkVehicleMenu();
                        break;

                    case "Ta bort fordon":
                        RemoveVehicleMenu();
                        break;

                    case "Sök fordon":
                        SearchVehicleMenu();
                        break;

                    case "Visa P-hus":
                        ShowParkingMap();
                        break;
                    case "Spara":
                        _fileManager.Save(_garage);
                        AnsiConsole.MarkupLine("[green]✓ Garage sparat[/]");
                        break;
                    case "Avsluta":
                        return;
                }
                       
            }
        }
        //Menyval 1
        private void ParkVehicleMenu()
        {
          

            var vehicleType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Vilken fordonstyp")
                .AddChoices(
                        "Bil",
                        "MC"));

            string regNumber = AnsiConsole.Ask <string> ("Registreringsnummer?")
                .ToUpper()
                .Trim();

            Vehicle vehicle;
            if(vehicleType == "Bil")
            {
                vehicle = new Car();
            }
            //Här måste vi komma ihåg att vi behöver ändra else statement om vi lägger till fler fordon senare
            else
            {
                vehicle = new Motorcycle();
            }
            vehicle.RegNumber = regNumber;
            ParkingSpot? parkingSpot = _garage.ParkVehicle(vehicle);

            if(parkingSpot != null)
            {
                _fileManager.Save(_garage);
                AnsiConsole.MarkupLine($"[green bold]✓ Fordonet med registreringsnummer {regNumber} har parkerats på plats {parkingSpot.SpotNumber}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red bold]Ingen ledig plats hittades för fordonet med registreringsnummer {regNumber}.[/]");
            }
        }
        // Menyval 2
        private void RemoveVehicleMenu()
        {
            string regNumber = AnsiConsole.Ask<string>("Regristeringsnummer?")
                .ToUpper()
                .Trim();
            
            Vehicle? removedVehicle = _garage.RemoveVehicle(regNumber);                 //Frågetecken då vi inte vet om värdet blir null
            
         
            if (removedVehicle != null)
            {
                decimal price = CalculatePrice(removedVehicle);

                _fileManager.Save(_garage);

                AnsiConsole.MarkupLine($"[green bold]✓ Fordon med regristeringsnummer {regNumber} har tagits bort.[/]");
                AnsiConsole.MarkupLine($"[yellow]Kostnad:[/] {price} CZK");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red bold]Inget fordon med regristeringsnummer {regNumber} hittades.[/]");
            }
        }
        //Menyval 3
        private void SearchVehicleMenu()
        {
            string regNumber = AnsiConsole.Ask<string>("Regristeringsnummer?")
                .ToUpper()
                .Trim();

            ParkingSpot? spot = _garage.SearchVehicleSpot(regNumber);

            if(spot != null)
            {
                AnsiConsole.MarkupLine($"[green bold]Fordon med regristeringsnummer {regNumber} hittades på plats: {spot.SpotNumber}[/]"); //Behöver få in plats på något sätt här
            }
            else
            {
                AnsiConsole.MarkupLine($"[red bold]Fordon med regristeringsnummer {regNumber} hittades inte i systemet.[/]");
            }
        }

        //Menyva 4
        public void ShowParkingMap()
        {
            int availableSpots = 0;
            foreach(ParkingSpot spot in _garage.ParkingSpots)
            {
                if(spot.GetOccupiedSize() == 0)                            //Kollar antal lediga platser
                {
                    availableSpots++;
                }
            }
            string map = "";

            foreach(ParkingSpot spot in _garage.ParkingSpots)               //Använder mig av emojis för att visa lediga/delvis/upptagna platser
            {
                string status = "";

                if(spot.GetOccupiedSize() == 0)
                {
                    status = "🟩";
                }
                else if(spot.GetOccupiedSize() == spot.Size)
                {
                    status = "🟥";
                }
                else
                {
                    status = "🟨";
                }
                map += $"{spot.SpotNumber:000} {status}  ";

                if(spot.SpotNumber % 10 == 0)
                {
                    map += "\n";
                }
            }
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("🟩 Ledig");
            AnsiConsole.WriteLine("🟨 Delvis upptagen");
            AnsiConsole.WriteLine("🟥 Full");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[blue bold underline]Parkeringshus[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[green]Lediga platser:[/] {availableSpots}/{_garage.ParkingSpots.Count}");
            AnsiConsole.WriteLine();

            Console.WriteLine(map);
        }

        
        private decimal CalculatePrice(Vehicle vehicle)
        {
            TimeSpan parkingTime =
                vehicle.DepartureTime - vehicle.ArrivalTime;

            if (parkingTime.TotalMinutes <= _priceList.FreeParkingMinutes)
            {
                return 0;
            }

            int hours =
                (int)Math.Ceiling(parkingTime.TotalHours);

            if (vehicle.VehicleType == "Car")
            {
                return hours * _priceList.CarParkingPrice;
            }

            return hours * _priceList.MotorcycleParkingprice;
        }


    }
}
