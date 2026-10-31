using System.Text.Json;
using System.IO;


namespace PragueParking.Data
{
    public class FileManager
    {
        public void Save(ParkingGarage garage)
        {
            string json = JsonSerializer.Serialize(garage);

            File.WriteAllText("garage.json", json);
        }

        public ParkingGarage? Load()
        {
            string json = File.ReadAllText("garage.json");

            ParkingGarage? garage = JsonSerializer.Deserialize<ParkingGarage>(json);

            return garage;

        }

        public PriceList LoadPrices()
        {
            PriceList priceList = new PriceList();
            string[] lines = File.ReadAllLines("PriceList.txt");

            foreach(string line in lines)
            {
                if(line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split("=");

                switch(parts[0])
                {
                    case "Car":
                        priceList.CarParkingPrice = decimal.Parse(parts[1]);
                        break;

                    case "Motorcycle":
                        priceList.MotorcycleParkingprice = decimal.Parse(parts[1]);
                        break;

                    case "FreeMinutes":
                        priceList.FreeParkingMinutes = int.Parse(parts[1]);
                        break;
                }
            }

            return priceList;
        }

        public Config LoadConfig()
        {
            string json = File.ReadAllText("Config.json");

            Config? config = JsonSerializer.Deserialize<Config>(json);

            return config;
        }
    }
}
