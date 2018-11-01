using NUnit.Framework;
using GeoGo.Model;
using System.Collections.Generic;

namespace GeoGo.Tests.ModelTest
{

    public class LocalDatabaseTest
    {

        [SetUp]
        public void BeforeEachTest(){

        }

        [Test()]
        public void CanInsertNewGeoDataToDatabase()
        {
            List<Coordinate> coordinateList = new List<Coordinate>{
            new Coordinate(100.0,100.0),
            new Coordinate(200.0,200.0),
            new Coordinate(300.0,300.0)
            };

            Assert.AreEqual(LocalDatabase.InsertNewGeodataToDB(coordinateList,
                            new GeoData("fakePoint", "fakeType", "faker", "fakeDescription")), "Success");
        }

        [Test()]
        public void CanRetrieveAlltheGeoDataFromDatabase(){
            Assert.IsNotNull(LocalDatabase.GetAllGeoDataSet()); 
        }

        [Test()]
        public void CanDeleteAlltheGeoDataFromDatabase()
        {
            LocalDatabase.CleanAllDataInTable();
            Assert.AreEqual(LocalDatabase.GetAllGeoDataSet().Count, 0);
        }


    }
}