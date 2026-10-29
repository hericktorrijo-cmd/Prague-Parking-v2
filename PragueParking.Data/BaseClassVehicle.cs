using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PragueParking.Data
{
    public class Vehicle
    {
        //Lägger en tom sträng annars varnas jag pga när en barnklass objekt initieras kan den vara tom
        private string _regNumber = "";
        

        public string RegNumber
        {
            get { return _regNumber; }
            set {  _regNumber = value; }
        }

        public string VehicleType { get; set; } = "";

        public DateTime ArrivalTime { get; set; }

        public DateTime DepartureTime { get; set;  }

        public int Size { get; set; }

        


        
        public void SetArrivalTime()  
        {
            ArrivalTime = DateTime.Now; 
        }
        
        public void SetDepartureTime() 
        {
            DepartureTime = DateTime.Now;
        }

        
    }
}
