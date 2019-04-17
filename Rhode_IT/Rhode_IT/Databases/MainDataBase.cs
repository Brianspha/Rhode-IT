using Plugin.SecureStorage;
using Realms;
using Realms.Sync;
using Rhode_IT.Classes;
using Rhode_IT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Maps;

namespace Rhode_IT.Databases
{
    public class MainDataBase
    {

        Realm db;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RealmDataBase"/> class.
        /// </summary>
        public MainDataBase()
        {
            try
            {

                db = Realm.GetInstance();//open realm db

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

            db.Write(() => {
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
        public void saveLogins(LoginDetails dets)
        {
            CrossSecureStorage.Current.SetValue("Password", dets.password);
            CrossSecureStorage.Current.SetValue("ID", dets.userID);
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
            CrossSecureStorage.Current.DeleteKey("Password");
            CrossSecureStorage.Current.DeleteKey("ID");

        }
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
        public int addDockingStation(string[] names)
        {
            int added = -1;
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
                                added = 1;
                                //@dev TODO sync with Blockchain
                            }
                            else
                            {
                                added = 0;
                            }
                        }
                    }
                }
            }
            return added;
        }
        public List<DockingStaionPin> getAllDockingStations()
        {
            List<DockingStaionPin> stationPins = new List<DockingStaionPin>();
            var stations = db.All<DockingStations>().ToList()[0];
            if(stations != null)
            {
                foreach(var station in stations.RegisteredDockingStaions)
                {
                    stationPins.Add(new DockingStaionPin { Position = new Position(station.DockingStationInformation.Lat, station.DockingStationInformation.Long), Label = station.DockingStationInformation.Name });
                }
            }
            return stationPins;
        }
    }
}
