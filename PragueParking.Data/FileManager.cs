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
    }
}
