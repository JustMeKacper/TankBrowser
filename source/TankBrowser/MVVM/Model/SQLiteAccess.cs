using Microsoft.Data.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;

namespace TankBrowser.MVVM.Model
{
    public class SQLiteAccess
    {
        //[PrimaryKey,AutoIncrement]
        //public int Id { get; set; }
        //public string TankName { get; set; }
        //public string TankDescription { get; set; }
        //public string TankPhotoName { get; set; }

        static string DbName = "TanksDB.db";
        static string FolderPath = @"C:\Users\repca\Desktop\Studia\TankBrowser\Debug";
        public static string _DbPath = Path.Combine(FolderPath, DbName);

        private static string _ConnectionString = $"Data Source= {_DbPath}";

        public static bool CreateTableSheet()
        {
            bool isTableCreated = false;
            SqliteConnection dbConnection = new SqliteConnection(_ConnectionString);
            dbConnection.Open();

            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = "CREATE TABLE IF NOT EXIST Places(Id INTEGER PRIMARY KEY AUTOINCREMENT, TankName TEXT, " +
                    "TankName TEXT, TankFileName Text";
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                int result = dbCommand.ExecuteNonQuery();
                if (result == 0)
                    isTableCreated = true;
            }
            dbConnection.Close();
            return isTableCreated;
        }


        public static bool InsertTank(Tank newTank)
        {
            bool isInserted = false;
            SqliteConnection dbConnection = new SqliteConnection(_ConnectionString);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = $"SELECT COUNT(Id) FROM Tanks WHERE TankName='{newTank.Name}'";
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                int result = Convert.ToInt32(dbCommand.ExecuteScalar());
                if (result==0)
                {
                    dbQuery = $"INSERT INTO Tanks Values(null,'{newTank.Name}','{newTank.Description}','{newTank.ImageName}')";
                    dbCommand.CommandText = dbQuery;
                    dbCommand.ExecuteNonQuery();
                        isInserted = true;
                }
            }
            dbConnection.Close();
            return isInserted;
        }

        public static bool DeleteTank(Tank tankToDelete)
        {
            bool isDeleted = false;
            SqliteConnection dbConnection = new SqliteConnection(_ConnectionString);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = $"SELECT COUNT(Id) FROM Tanks WHERE TankName='{tankToDelete.Name}'";
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                int result = Convert.ToInt32(dbCommand.ExecuteScalar());
                if (result==1)
                {
                    dbQuery = $"DELETE FROM Tanks WHERE TankDesc='{tankToDelete.Description}'";
                    dbCommand.CommandText = dbQuery;
                    if (dbCommand.ExecuteNonQuery() == 1)
                        isDeleted = true;
                }
            }
            dbConnection.Close();
            return isDeleted;
        }


        public static List<Tank> ReadAllTanks()
        {
            List<Tank> tankList = new List<Tank>();
            SqliteConnection dbConnection = new SqliteConnection(_ConnectionString);
            dbConnection.Open();
            if(dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = "SELECT * FROM Tanks";
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                SqliteDataReader dbDataReader = dbCommand.ExecuteReader();
                while (dbDataReader.Read())
                {
                    Tank tmpTank = new Tank(dbDataReader["TankName"].ToString(), dbDataReader["TankDesc"].ToString(), dbDataReader["TankFileName"].ToString());
                    tankList.Add(tmpTank);
                }
            }
            dbConnection.Close();
            return tankList;
        }


        public static bool UpdateTankData(Tank tank2Update)
        {
            bool isUpdated = false;
            SqliteConnection dbConnection = new SqliteConnection(_ConnectionString);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = $"SELECT COUNT(Id) FROM Tanks WHERE TankName='{tank2Update.Name}'";
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                int result = Convert.ToInt32(dbCommand.ExecuteScalar());
                if (result==1)
                {
                    dbQuery = $"UPDATE Tanks SET TankDesc = '{tank2Update.Description}', TankFileName = '{tank2Update.ImageName}' WHERE TankName = '{tank2Update.Name}'";
                    dbCommand.CommandText = dbQuery;
                    if (dbCommand.ExecuteNonQuery() == 1)
                        isUpdated = true;
                }

            }
            dbConnection.Close();
            return isUpdated;
        }
    }
}
