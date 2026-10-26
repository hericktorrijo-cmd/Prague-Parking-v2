using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_v2
{
    internal abstract class Vehicle
    {
        private string _regNumber;

        public string RegNumber
        {
            get { return _regNumber; }
            set {  _regNumber = value; }
        }

        public DateTime ArrivalTime { get; set; }

        public int Size { get; protected set; }
    }
}
