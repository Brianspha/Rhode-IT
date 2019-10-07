using Plugin.SecureStorage;
using Realms;
using RhodeIT.Classes;
using RhodeIT.Models;
using RhodeIT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.GoogleMaps;

namespace RhodeIT.Databases
{
    public class LocalDataBase
    {
        #region Local database function
        private Realm db;
        private readonly RhodeITSmartContract RhodeITSmartContract;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RealmDataBase"/> class.
        /// </summary>
        public LocalDataBase()
        {
            try
            {

                db = Realm.GetInstance();//open realm db
                RhodeITSmartContract = new RhodeITSmartContract();

            }
            catch (Realms.Exceptions.RealmMigrationNeededException)
            {

            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// Checks if the App has been launched before .
        /// </summary>
        /// <returns><c>true</c>, if run was firsted, <c>false</c> otherwise.</returns>
        public bool FirstRun()
        {
            List<VenueLocation> temp = db.All<VenueLocation>().ToList();
            return temp.Count == 0 ? true : false;
        }
        /// <summary>
        /// Stores all the Lecture Venue locations on Rhodes Campus.
        /// </summary>
        /// <param name="locs">Locs.</param>
        public void storeVenueLocations(List<VenueLocation> locs)
        {

            db.Write(() =>
            {
                List<VenueData> loc = db.All<VenueData>().ToList();
                if (loc.Count == 0) //first time app is used create a new VenueData object
                {
                    VenueData VenueD = new VenueData();
                    for (int i = 0; i < locs.Count; i++)
                    {
                        VenueD.Venues.Add(locs[i]);
                    }

                    db.Add(VenueD);
                    db.Refresh();
                }
                else
                {
                    VenueData temp = loc[0];
                    temp.Venues.Clear();//clear the current list and add the updated venues
                    for (int i = 0; i < locs.Count; i++)
                    {
                        temp.Venues.Add(locs[i]);
                    }

                    db.Add(temp);
                    db.Refresh();
                }
            });
        }
        /// <summary>
        /// @Dev saves user logins to 
        /// </summary>
        /// <param name="dets">login credentials of user</param>
        public void Login(LoginDetails dets)
        {
            CrossSecureStorage.Current.SetValue("Password", dets.Password);
            CrossSecureStorage.Current.SetValue("ID", dets.User_ID);
            db.Write(() =>
            {
                dets.Password = "";//@dev we dont store password in realm db since we keeping that in securestorage
                db.Add(dets, true);
            });
            Console.WriteLine("Logged in");
        }
        /// <summary>
        /// @dev checks to see if the user had logged in before if yes we return they credientials
        /// </summary>
        /// <returns>user logins</returns>
        public LoginDetails hasLoggedInBefore()
        {
            LoginDetails temp = new LoginDetails { User_ID = CrossSecureStorage.Current.GetValue("ID"), Password = CrossSecureStorage.Current.GetValue("Password") };
            return temp;
        }
        /// <summary>
        /// @Dev deletes user login credentials everytime they loggout
        /// </summary>
        public void logOut()
        {
            //CrossSecureStorage.Current.DeleteKey("Password");
            //CrossSecureStorage.Current.DeleteKey("ID");
            Console.WriteLine("Logging out");

        }
        /// <summary>
        /// @
        /// </summary>
        /// <param name="config"></param>
        public void saveIPConfig(IPConfig config)
        {
            db.Write(() =>
            {
                db.Add(config);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IPConfig getCurrentDockingStationIP()
        {
            List<IPConfig> IP = db.All<IPConfig>().ToList<IPConfig>();
            return IP.ElementAt(0);
        }

        /// <summary>
        /// @dev fetches all avaiable bicycles using the passed in venue name
        /// </summary>
        /// <param name="dockingStationName"></param>
        /// <returns></returns>
        public ObservableCollection<Bicycle> getAvailableBicycles(string dockingStationName)
        {
            ObservableCollection<Bicycle> temp = new ObservableCollection<Bicycle>();
            ///@dev fetch biycles from 



            return temp;
        }

        /// <summary>
        /// @dev looks for venue using name returns the Latitude and Longitude position of venue 
        /// </summary>
        /// <param name="name">Name of the venue we look for</param>
        /// <returns></returns>
        public Position getDockingStationLatLong(string name)
        {
            IQueryable<VenueLocation> venue = db.All<VenueLocation>().Where(v => v.Name == name);
            return new Position(venue.ToList()[0].Latitude, venue.ToList()[0].Longitude);
        }
        /// <summary>
        /// @dev returns the number of available bicycles in a given Docking station
        /// </summary>
        /// <param name="venueName"></param>
        /// <returns>number of available bicycles</returns>
        public int getAvailableBicyclesCount(string venueName)
        {
            return db.All<DockingStaion>().Where(station => station.DockingStationInformation.Name == venueName).ToList().Count;

        }

      

        /// <summary>
        /// @dev gets all registerd docking stations on platform
        /// </summary>
        /// <returns> all docking stations on platform</returns>
        public List<Pin> getAllDockingStations()
        {
            List<Pin> stationPins = new List<Pin>();
            DockingStations stations = db.All<DockingStations>().ToList()[0];
            string[] files = new string[] { "Icon1.png" };
            if (stations != null)
            {
                foreach (DockingStaion station in stations.RegisteredDockingStaions)
                {
                    Pin tempPin = new Pin() { Position = new Position(station.DockingStationInformation.Latitude, station.DockingStationInformation.Longitude), Label = station.DockingStationInformation.Name, Address = "Available Bicycles: " + station.AvailableBicycles.Count.ToString() };
                    Assembly assembly = typeof(MapsTab).GetTypeInfo().Assembly;
                    string file = files[0];
                    string[] names = assembly.GetManifestResourceNames();
                    tempPin.Icon = BitmapDescriptorFactory.FromBundle(file);
                    stationPins.Add(tempPin);
                }
            }
            return stationPins;
        }
        #endregion
        #region Communicate with Blockchain Functions
        #endregion
    }
}
