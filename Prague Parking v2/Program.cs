namespace Prague_Parking_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p1 = new ParkingGarage(100);
            Console.WriteLine(p1.ParkingSpots.Count);
        }
    }
}
