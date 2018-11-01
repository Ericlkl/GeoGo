using NUnit.Framework;
using GeoGo.Model;

namespace GeoGo.Tests.ModelTest
{
    [TestFixture()]
    public class CoordinateTest
    {
        Coordinate coordinate;

        [SetUp]
        public void SetUp()
        {
            coordinate = new Coordinate(100.0, 200.0);
        }

        [Test()]
        public void CanReadLatitude()
        {
            Assert.AreEqual(coordinate.Latitude, 100.0);
        }
        [Test()]
        public void CanReadLongitude()
        {
            Assert.AreEqual(coordinate.Longitude, 200.0);
        }

        [Test()]
        public void CanUpdateLatitude()
        {
            coordinate.Latitude = 340.5;
            Assert.AreEqual(coordinate.Latitude, 340.5);
        }
        [Test()]
        public void CanUpdateLongitude()
        {
            coordinate.Longitude = 367.5;
            Assert.AreEqual(coordinate.Longitude, 367.5);
        }

    }
}
