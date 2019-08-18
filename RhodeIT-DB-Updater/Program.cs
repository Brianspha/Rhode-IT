using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RhodeIT.Helpers;
using Npgsql;
namespace RhodeIT_DB_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceHelper helper = new ResourceHelper("C://Users//g14m1190//Documents//RhodeIT//RhodeIT//RhodeIT.Android//Assets//RhodesMap.geojson", "C://Users//g14m1190//Documents//RhodeIT//RhodeIT//RhodeIT.Android//Assets//Venues.txt");
            var venues = helper.GetParsedVenuesWithSubjects();
            string values = "VALUES";
            for (int i = 0; i < venues.Count; i++)
            {
                var venue = venues[i];
                values += "(" + "'" + venue.Name + "'" + "," + "'" + venue.Lat + "'" + "," + "'" + venue.Long + "'" + ",not registered as docking station" + ")";
                if (i < venues.Count - 1)
                {
                    values += ",";
                }
            }
            using (NpgsqlConnection connection = new NpgsqlConnection("POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;Server=146.231.123.137; Port=5433; User Id=postgres; Password=spha; Database=rhodesdb; Timeout=300;"))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO dockingstations (name,latitude,longitude,registrationhash) " + values + ";", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
