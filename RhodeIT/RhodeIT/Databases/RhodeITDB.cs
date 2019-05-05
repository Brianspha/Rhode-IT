using Newtonsoft.Json;
using Npgsql;
using Plugin.SecureStorage;
using Realms;
using Realms.Sync;
using RhodeIT.Classes;
using RhodeIT.Helpers;
using RhodeIT.Models;
using RhodeIT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace RhodeIT.Databases
{
    public class RhodeITDB
    {
        #region Local database function
        Realm db;
        RhodeITSmartContract RhodeITSmartContract;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RealmDataBase"/> class.
        /// </summary>
        public RhodeITDB()
        {
            try
            {

                db = Realm.GetInstance();//open realm db
               // RhodeITSmartContract = new RhodeITSmartContract();

            }
            catch (Realms.Exceptions.RealmMigrationNeededException)
            {

            }
            catch (Exception e)
            {

            }
        }
        /// <summary>
        /// Checks if the App has been launched before .
        /// </summary>
        /// <returns><c>true</c>, if run was firsted, <c>false</c> otherwise.</returns>
        public bool FirstRun()
        {
            var temp = db.All<VenueLocation>().ToList();
            return temp.Count == 0 ? true : false;
        }
        /// <summary>
        /// Stores all the Lecture Venue locations on Rhodes Campus.
        /// </summary>
        /// <param name="locs">Locs.</param>
        public void storeVenueLocations(List<VenueLocation> locs)
        {
            //string jsonData = "";
            //foreach (var location in locs)
            //{
            //    jsonData += JsonConvert.SerializeObject(location);
            //}
            //jsonData = jsonData.Replace("\"", "'");
            //using (NpgsqlConnection connection = new NpgsqlConnection(Variables.connectionStringRhodeITDB))
            //{
            //    connection.Open();
            //    NpgsqlCommand command = new NpgsqlCommand("INSERT INTO venuelocations (venue) values(" +  "'" + jsonData + "'" + ");", connection);
            //    command.ExecuteNonQuery();
            //}

            db.Write(() =>
            {
                var loc = db.All<VenueData>().ToList();
                if (loc.Count == 0) //first time app is used create a new VenueData object
                {
                    var VenueD = new VenueData();
                    for (int i = 0; i < locs.Count; i++) VenueD.Venues.Add(locs[i]);
                    db.Add(VenueD);
                    db.Refresh();
                }
                else
                {
                    var temp = loc[0];
                    temp.Venues.Clear();//clear the current list and add the updated venues
                    for (int i = 0; i < locs.Count; i++) temp.Venues.Add(locs[i]);
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
            CrossSecureStorage.Current.SetValue("Password", dets.password);
            CrossSecureStorage.Current.SetValue("ID", dets.userID);
            db.Write(() =>
            {
                dets.password = "";//@dev we dont store password in realm db since we keeping that in securestorage
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
            LoginDetails temp = new LoginDetails { userID = CrossSecureStorage.Current.GetValue("ID"), password = CrossSecureStorage.Current.GetValue("Password") };
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
            var IP = db.All<IPConfig>().ToList<IPConfig>();
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
            var venue = db.All<VenueLocation>().Where(v => v.Name == name);
            return new Position(venue.ToList()[0].Lat, venue.ToList()[0].Long);
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
        /// @dev adds a venue/venues as Docking stations
        /// stores venue/venues on the Blockchain database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="names"></param>
        /// <returns>returns 1  if added succesfully -1 if not 0 if already exist 0 if no paramaters were provided</returns>
        public async Task<bool> addDockingStationAsync(string[] names)
        {
            List<VenueLocation> ven = new List<VenueLocation>();
             if (names.Length>0)
            {
                var test = db.All<VenueLocation>().ToList();
                foreach(VenueLocation loc in test)
                {
                    Console.WriteLine(loc.Name);
                }
                foreach (string name in names)
                {
                    var venue = db.All<VenueLocation>().Where(v => v.Name.Contains(name));
                    if (venue.ToList().Count > 0)
                    {
                        var dockingStations = db.All<DockingStations>().ToList();
                        if (dockingStations.Count == 0)
                        {
                            //@dev TODO sync with Blockchain
                            DockingStations stations = new DockingStations();
                            stations.RegisteredDockingStaions.Add(new DockingStaion { DockingStationInformation = venue.ToList()[0] });
                            db.Write(() => {
                                db.Add(stations);
                            });
                            //@dev 
                            ven.Add(venue.ToList()[0]);
                        }
                        else
                        {
                            var exists = dockingStations.ToList()[0].RegisteredDockingStaions.ToList().Find(station => station.DockingStationInformation.Name.Contains(name));
                            if (exists == null)
                            {
                                db.Write(() =>
                                {
                                    dockingStations.ToList()[0].RegisteredDockingStaions.Add(new DockingStaion { DockingStationInformation = venue.ToList()[0] }); 
                                    db.Add(dockingStations[0], true);
                                });
                                ven.Add(venue.ToList()[0]);

                                //@dev TODO sync with Blockchain
                            }
                        }
                    }
                }
            }
             if(ven.Count>0)
            {
               await syncLatLongWithBlockchain(ven);
            }
            return await Task.FromResult<bool>(ven.Count > 0);
        }

        /// <summary>
        /// @dev gets all registerd docking stations on platform
        /// </summary>
        /// <returns> all docking stations on platform</returns>
        public List<Pin> getAllDockingStations()
        {
            List<Pin> stationPins = new List<Pin>();
            var stations = db.All<DockingStations>().ToList()[0];
            string[] files = new string []{ "Icon1.png" };
            if(stations != null)
            {
                foreach(var station in stations.RegisteredDockingStaions)
                {
                    var tempPin = new Pin() { Position = new Position(station.DockingStationInformation.Lat, station.DockingStationInformation.Long), Label =station.DockingStationInformation.Name, Address = "Available Bicycles: " + station.AvailableBicycles.Count.ToString() };
                    var assembly = typeof(MapsTab).GetTypeInfo().Assembly;
                    var file = files[0];
                    string[] names = assembly.GetManifestResourceNames();
                    tempPin.Icon = BitmapDescriptorFactory.FromBundle(file);
                    stationPins.Add(tempPin);
                }
            }
            return stationPins;
        }
#endregion
        #region Communicate with Blockchain Functions
        /// <summary>
        /// @dev stores new venue to smart contract
        /// </summary>
        /// <param name="venue"></param>
        private async Task syncLatLongWithBlockchain(List<VenueLocation> venues)
        {
            RhodeITSmartContract = new RhodeITSmartContract();
            await RhodeITSmartContract.registerDockingStation(venues);
        }
        private async Task<bool> addUser(string stdNo,string password)
        {
            RhodeITSmartContract = new RhodeITSmartContract();
            bool added = await RhodeITSmartContract.Login(stdNo, password);
            Console.WriteLine("Logged in: ", added);
            return added;
        }
        #endregion 
    }
}
