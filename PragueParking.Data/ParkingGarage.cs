using System;
using System.Collections.Generic;
using System.Text;

namespace PragueParking.Data
{
    public class ParkingGarage
    {
        public List<ParkingSpot> ParkingSpots {  get; set; }

        public ParkingGarage(int numberOfSpots)
        {
            ParkingSpots = new List<ParkingSpot>();

            for(int i = 1; i <= numberOfSpots; i++)
            {
                ParkingSpots.Add(new ParkingSpot(4, i));
            }
        }

        public ParkingGarage()
        {
            ParkingSpots = new List<ParkingSpot>();
        }

        //Parkera fordon metod
        public ParkingSpot? ParkVehicle(Vehicle vehicle)               
        {
            foreach(ParkingSpot spot in ParkingSpots)
            {
                if(spot.CanFitVehicle(vehicle))
                {
                    spot.Vehicles.Add(vehicle);
                    //Använder mig av metoden för att sätta tiden då egenskapen protected i Vehicle klass.
                    vehicle.SetArrivalTime();  
                    return spot;
                }
            }
            return null;
        }

        //Metod för att ta bort ett fordon, Sätter ett frågetecken i metoden så att det är möjligt att returnera null om vi inte hittar Vehicle
        public Vehicle? RemoveVehicle(string regNumber)                     
        {
            foreach(ParkingSpot spot in ParkingSpots)
            {
                for(int i = 0; i < spot.Vehicles.Count; i++)
                {
                    if (spot.Vehicles[i].RegNumber == regNumber)
                    {
                        Vehicle vehicle = spot.Vehicles[i];
                        vehicle.SetDepartureTime();
                        spot.Vehicles.RemoveAt(i);
                        return vehicle;
                    }
                }
            }
            return null;
        }

        //Metod för att leta efter ett fordon, Sätter ett frågetecken i metoden så att det är möjligt att returnera null om vi inte hittar Vehicle
        public Vehicle? SearchVehicle(string regNumber)
        {
            foreach(ParkingSpot spot in ParkingSpots)
            {
                foreach(Vehicle vehicle in spot.Vehicles)
                {
                    if (vehicle.RegNumber == regNumber)
                    {
                        return vehicle;
                    }
                }
            }

            return null;
        }

        //Metod för att leta fram platsen och inte regnumret
        public ParkingSpot? SearchVehicleSpot(string regNumber)
        {
            foreach(ParkingSpot spot in ParkingSpots)
            {
                foreach(Vehicle vehicle in spot.Vehicles)
                {
                    if (vehicle.RegNumber == regNumber)
                    {
                        return spot;
                    }
                }
            }
            return null;
        }
    }
}
