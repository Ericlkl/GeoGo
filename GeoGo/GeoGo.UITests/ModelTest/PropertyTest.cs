using NUnit.Framework;
using System;
using GeoGo.Model;

namespace GeoGo.UITests.ModelTest
{
    [TestFixture()]
    public class PropertyTest
    {
        Property property;

        [SetUp]
        public void SetUp()
        {
            property = new Property("Height", "1200");
        }

        [Test()]
        public void CanReadPropertyName()
        {
            Assert.AreEqual(property.PropertyName, "Height");
        }
        [Test()]
        public void CanReadPropertyValue()
        {
            Assert.AreEqual(property.PropertyValue, "1200");
        }

        [Test()]
        public void CanUpdatePropertyName()
        {
            property.PropertyName = "Width";
            Assert.AreEqual(property.PropertyName, "Width");
        }
        [Test()]
        public void CanUpdatePropertyValue()
        {
            property.PropertyValue = "200";
            Assert.AreEqual(property.PropertyValue, "200");
        }
    }
}
