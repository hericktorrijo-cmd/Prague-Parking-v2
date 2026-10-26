using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_v2
{
    internal class ParkingGarage
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
    }
}
