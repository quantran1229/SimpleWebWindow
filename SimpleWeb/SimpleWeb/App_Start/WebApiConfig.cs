using Microsoft.Data.Sqlite;
using System;
using System.Diagnostics;
using System.IO;
using System.Web.Http;

namespace SimpleWeb
{
    public static class WebApiConfig
    {
        private const string DB_NAME = "SimpleDatabase.sqlite";
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Insert(0, new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            Newtonsoft.Json.JsonConvert.DefaultSettings = () => new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            Debug.WriteLine(AppDomain.CurrentDomain.BaseDirectory + DB_NAME);
            //check if database exist? if not then create one
            createNewDatabase();
        }

        //create SimpleWeb database
        private static void createNewDatabase()
        {

                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + DB_NAME))
                {
                    Debug.WriteLine("Create new database");
                    SqliteConnection SQLconnect = new SqliteConnection();
                    
                    SQLconnect.ConnectionString = "Data Source=" +AppDomain.CurrentDomain.BaseDirectory+DB_NAME;
                    SQLconnect.Open();
                    SQLconnect.Close();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + DB_NAME))
                    {
                        createTable();
                        Debug.WriteLine("Finishing created new database");
                    }
                }
                else
                {
                    Debug.WriteLine("database exist!!!");
                }
            }

        //create table for database
        private static void createTable()
        {

                using (SqliteConnection db_conn = new SqliteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + DB_NAME + ";"))
                {

                    db_conn.Open();
                    string sql = "create table Users (username varchar(50) NOT NULL primary key,password varchar(50) NOT NULL,firstname varchar(100),lastname varchar(100),email varchar(50) NOT NULL)";
                    SqliteCommand command = new SqliteCommand(sql, db_conn);
                    command.ExecuteNonQuery();
                    Debug.WriteLine("Created Users table");
                    db_conn.Close();
                }
            }

    }
}
