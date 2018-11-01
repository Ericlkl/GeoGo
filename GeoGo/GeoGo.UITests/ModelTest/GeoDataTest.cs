using GeoGo.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace GeoGo.Tests.ModelTest
{
    [TestFixture()]
    public class GeoDataTest
    {
        GeoData GeoDataPointInstance;
        GeoData GeoDataLineInstance;
        GeoData GeoDataPolygonInstance;

        List<Coordinate> coordinateList = new List<Coordinate>{
            new Coordinate(100.0,100.0),
            new Coordinate(200.0,200.0),
            new Coordinate(300.0,300.0)
        };

        [SetUp]
        public void SetUp()
        {
            GeoDataPointInstance = new GeoData("fakePoint","fakeType","faker","fakeDescription");
            GeoDataLineInstance = new GeoData("fakeLine", "fakeType", "faker", "fakeDescription");
            GeoDataPolygonInstance = new GeoData("fakePolygon", "fakeType", "faker", "fakeDescription");

            GeoDataPointInstance.InsertCoordinate(new List<Coordinate>{
                new Coordinate(100.0,100.0)});

            GeoDataLineInstance.InsertCoordinate(new List<Coordinate>{
                new Coordinate(100.0,100.0),
                new Coordinate(200.0,200.0)});

            GeoDataPolygonInstance.InsertCoordinate(new List<Coordinate>{
                new Coordinate(100.0,100.0),
                new Coordinate(200.0,200.0),
                new Coordinate(300.0,300.0)});

        }

        [Test()]
        public void CreateGeoDataObjectTest()
        {
            Assert.AreEqual(GeoDataPointInstance.Name, "fakePoint");
            Assert.AreEqual(GeoDataPointInstance.Type, "fakeType");
            Assert.AreEqual(GeoDataPointInstance.Description, "fakeDescription");
            Assert.AreEqual(GeoDataPointInstance.Provider, "faker");
        }

        [Test()]
        public void CanInsertNewPropertiy()
        {
            GeoDataPointInstance.InsertProperty(new Property("Height", "50"));

            GeoDataPointInstance.Properties.ForEach((obj) =>
            {
                Assert.AreEqual(obj.PropertyName, "Height");
                Assert.AreEqual(obj.PropertyValue, "50");
            });
        }

        [Test()]
        public void CanExistMoreThanOnePropertiy()
        {
            GeoDataLineInstance.InsertProperty(new Property("Height","50"));
            GeoDataLineInstance.InsertProperty(new Property("Width", "100"));

            Assert.AreEqual(GeoDataLineInstance.Properties[0].PropertyName, "Height");
            Assert.AreEqual(GeoDataLineInstance.Properties[0].PropertyValue, "50");

            Assert.AreEqual(GeoDataLineInstance.Properties[1].PropertyName, "Width");
            Assert.AreEqual(GeoDataLineInstance.Properties[1].PropertyValue, "100");
        }

        [Test()]
        public void CanInsertNewCoordinate()
        {

            // Point Instance testing
            Assert.AreEqual(GeoDataPointInstance.Coordinates[0].Latitude, 100.0);
            Assert.AreEqual(GeoDataPointInstance.Coordinates[0].Longitude, 100.0);

            // Line instance testing
            Assert.AreEqual(GeoDataLineInstance.Coordinates[0].Latitude, 100.0);
            Assert.AreEqual(GeoDataLineInstance.Coordinates[0].Longitude, 100.0);
            Assert.AreEqual(GeoDataLineInstance.Coordinates[1].Latitude, 200.0);
            Assert.AreEqual(GeoDataLineInstance.Coordinates[1].Longitude, 200.0);

            //Polygon instance testing
            Assert.AreEqual(GeoDataPolygonInstance.Coordinates[0].Latitude, 100.0);
            Assert.AreEqual(GeoDataPolygonInstance.Coordinates[0].Longitude, 100.0);
            Assert.AreEqual(GeoDataPolygonInstance.Coordinates[1].Latitude, 200.0);
            Assert.AreEqual(GeoDataPolygonInstance.Coordinates[1].Longitude, 200.0);
            Assert.AreEqual(GeoDataPolygonInstance.Coordinates[2].Latitude, 300.0);
            Assert.AreEqual(GeoDataPolygonInstance.Coordinates[2].Longitude, 300.0);
        }

        [Test()]
        public void CanDetectDifferentShape()
        {
            Assert.AreEqual(GeoDataPointInstance.GeometryShape, "Point");
            Assert.AreEqual(GeoDataLineInstance.GeometryShape, "Line");
            Assert.AreEqual(GeoDataPolygonInstance.GeometryShape, "Polygon");
        }

    }
}
