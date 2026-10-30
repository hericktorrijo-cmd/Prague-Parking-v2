using PragueParking.Data;

namespace PragueParking.Tests
{
    [TestClass]
    public class VehicleTests
    {
        [TestMethod]
        public void Car_Should_Have_Size_4()
        {
            Car car = new Car();

            Assert.AreEqual(4, car.Size);
        }

        [TestMethod]
        public void Motorcycle_Should_Have_Size_2()
        {
            Motorcycle motorcycle = new Motorcycle();

            Assert.AreEqual(2, motorcycle.Size);
        }
    }
}