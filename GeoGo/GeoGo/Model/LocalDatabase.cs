using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using Auth0.OidcClient;
namespace GeoGo.Model
{
    public static class LocalDatabase
    {

        // Initialize the SQLite database Table
        private static void CreateTables(SQLiteConnection db)
        {
            db.CreateTable<GeoData>();
            db.CreateTable<Coordinate>();
            db.CreateTable<Property>();
        }

        //Get ALL the Geodata from the database
        public static List<GeoData> GetAllGeoDataSet()
        {
            /* 
             * EveryTime we need to do something with the local database, we need to create connection and kill it
             * That is why we need to use using statement
             */

            using (SQLiteConnection db = new SQLiteConnection(App.DatabaseLocation))
            {
                CreateTables(db);
                return db.GetAllWithChildren<GeoData>();
            }
        }


        // Insert New Geodata into Geodata Table
        public static string InsertNewGeodataToDB(Coordinate coor, GeoData data)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.DatabaseLocation))
            {
                CreateTables(db);
                db.Insert(coor);

                int rows = db.Insert(data);

                data.InsertCoordinate(new List<Coordinate> { coor });

                db.UpdateWithChildren(data);

                return rows > 0 ? "Success" : "Fail";

            }
        }

        // Clean all the data in every table in the database
        public static void CleanAllDataInTable()
        {
            using (SQLiteConnection db = new SQLiteConnection(App.DatabaseLocation))
            {
                CreateTables(db);
                db.Execute("DELETE FROM GeoData");
                db.Execute("DELETE FROM Coordinate");
            }
        }

        // Save the property information which related to the geoddata to the database
        public static string InsertPropertyToGeodata(Property prop, GeoData data)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.DatabaseLocation))
            {
                CreateTables(db);
                int rows = db.Insert(prop);

                data.InsertProperty(prop);

                db.UpdateWithChildren(data);

                return rows > 0 ? "Success" : "Fail";

            }
        }

        // Return the Geodata searching it by index
        public static GeoData GetGeoDataById(int Id)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.DatabaseLocation))
            {
                return db.GetWithChildren<GeoData>(Id);
            }
        }

    }
}
