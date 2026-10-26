using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_v2
{
    internal class ParkingSpot
    {
        public int SpotNumber { get; set; }
        public int Size {  get; private set; }

        public List<Vehicle> Vehicles { get; private set; }

        public ParkingSpot(int size, int spotNumber)
        {
            Size = size;
            SpotNumber = spotNumber;

            Vehicles = new List<Vehicle>();
        }
    }
}
