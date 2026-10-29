using System;
using System.Collections.Generic;
using System.Text;

namespace PragueParking.Data
{
    public class ParkingSpot
    {
        public int SpotNumber { get; set; }
        public int Size {  get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public ParkingSpot(int size, int spotNumber)
        {
            Size = size;
            SpotNumber = spotNumber;

            Vehicles = new List<Vehicle>();
        }

        public ParkingSpot()
        {
            Vehicles = new List<Vehicle>();
        }

        //Lägger till bas-klassen size i occupiedSize 
        public int GetOccupiedSize()  
        {
            int occupiedSize = 0;
            foreach(Vehicle vehicle in Vehicles)
            {
                occupiedSize += vehicle.Size;
            }
            return occupiedSize;
        }

        /*Metod som returnerar en bool som säger true om occupiedSize plus argument Vehicle Size är mindre eller likamed parkeringsplatsens size
        så att vi vet om den nya Vehiclen får plats i parkeringsplatsen */
        public bool CanFitVehicle(Vehicle vehicle)  //
        {
            int occupiedSize = GetOccupiedSize();

            return occupiedSize + vehicle.Size <= Size;

        }
    }
}
