using NUnit.Framework;
using System;
using GeoGo.Model;

namespace GeoGo.UITests.ModelTest
{
    [TestFixture()]
    public class UserLocationTest
    {
        [SetUp]
        public void SetUp()
        {
            UserLocation.UpdateMyCoordinate();
        }

        [Test()]
        public void CanUpdateUserCurrentCoordinateTest()
        {
            UserLocation.UpdateMyCoordinate();
            Assert.IsNotNull(UserLocation.Latitude);
            Assert.IsNotNull(UserLocation.Longitude);
        }
    }
}
