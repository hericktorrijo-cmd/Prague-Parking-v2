using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Prague_Parking_v2
{
    public abstract class Vehicle
    {
        //Lägger en tom sträng annars varnas jag pga när en barnklass objekt initieras kan den vara tom
        private string _regNumber = "";     

        public string RegNumber
        {
            get { return _regNumber; }
            set {  _regNumber = value; }
        }

        public DateTime ArrivalTime { get; protected set; }

        public DateTime DepartureTime { get; protected set;  }

        public int Size { get; protected set; }


        //Metod så att tiden kan skapas även om egenskapen är protected ifrån en instans(via egenskap) av Vehicle.
        public void SetArrivalTime()  
        {
            ArrivalTime = DateTime.Now; 
        }
        //Metod så att tiden regristreras när Vehicle tas bort utan att man kan ändra det ifrån en instans(via egenskap) av Vehicle.
        public void SetDepartureTime() 
        {
            DepartureTime = DateTime.Now;
        }

        
    }
}
