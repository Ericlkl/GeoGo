using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;

namespace GeoGo.Model
{
    public static class LocalDatabase
    {
        // Initialize the SQLite database Table
        private static void CreateTables(SQLiteConnection db)
        {
            db.CreateTable<GeoData>();
            db.CreateTable<Coordinate>();
            db.CreateTable<Description>();
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

    }
}
