﻿using Plugin.SecureStorage;
using Realms;
using RhodeIT.Classes;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using RhodeIT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace RhodeIT.Databases
{
    public class RhodeITDB
    {
        #region Local database function
        private Realm db;
        private RhodeITSmartContract RhodeITSmartContract;
        RhodeITService RhodeITService;
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
            catch (Exception)
            {

            }
            finally
            {
            }
        }
        /// <summary>
        /// @dev Checks if the App has been launched before .
        /// </summary>
        /// <returns><c>true</c>, if run was firsted, <c>false</c> otherwise.</returns>
        public bool FirstRun()
        {
            List<DockingStaion> temp = db.All<DockingStaion>().ToList();
            return temp.Count == 0 ? true : false;
        }
        /// <summary>
        /// @dev checks if the app was launched for the first time by an admin
        /// </summary>
        /// <returns>bool </returns>
        public bool FirstRunAdmin()
        {
            List<VenueLocation> temp = db.All<VenueLocation>().ToList();
            return temp.Count == 0 ? true : false;
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
            LoginDetails temp = new LoginDetails();
            try
            {
                temp = new LoginDetails { userID = CrossSecureStorage.Current.GetValue("ID"), password = CrossSecureStorage.Current.GetValue("Password") };
            }
            catch
            {

            }
            finally
            {
                Console.WriteLine("Loggin in as admin");
            }
            return temp;
        }
        /// <summary>
        /// @Dev deletes user login credentials everytime they loggout
        /// </summary>
        public void logOut()
        {
            CrossSecureStorage.Current.DeleteKey("Password");
            CrossSecureStorage.Current.DeleteKey("ID");
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
        /// <returns>ObservableCollection<Bicycle></returns>
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
        /// @dev stores all venues passed into the function
        /// </summary>
        /// <param name="locations"> names of the docking stations</param>
        public void storeVenueLocations(List<VenueLocation> locations)
        {
            if (db.All<VenueLocation>().ToList().Count > 0)
            {
                return;
            }
            db.Write(() =>
            {
                foreach (VenueLocation location in locations)
                {
                    db.Add(location, true);
                }
            });
        }

        /// <summary>
        /// @dev stores all venues based on the given names as Docking stations
        /// </summary>
        /// <param name="names"> names of the docking stations</param>
        /// <returns>list of dockign stations</returns>
        public void storeDockingStations(string[] names)
        {
            List<VenueLocation> ven = new List<VenueLocation>();
            if (names.Length > 0)
            {
                List<VenueLocation> test = db.All<VenueLocation>().ToList();
                foreach (string name in names)
                {
                    IQueryable<VenueLocation> venue = db.All<VenueLocation>().Where(v => v.Name.Contains(name));
                    if (venue.ToList().Count > 0)
                    {
                        List<DockingStations> dockingStations = db.All<DockingStations>().ToList();
                        if (dockingStations.Count == 0)
                        {
                            DockingStations stations = new DockingStations();
                            stations.RegisteredDockingStaions.Add(new DockingStaion { DockingStationInformation = venue.ToList()[0] });
                            db.Write(() =>
                            {
                                db.Add(stations);
                            });
                            //@dev 
                            ven.Add(venue.ToList()[0]);
                        }
                        else
                        {
                            DockingStaion exists = dockingStations.ToList()[0].RegisteredDockingStaions.ToList().Find(station => station.DockingStationInformation.Name.Contains(name));
                            if (exists == null)
                            {
                                db.Write(() =>
                                {
                                    dockingStations.ToList()[0].RegisteredDockingStaions.Add(new DockingStaion { DockingStationInformation = venue.ToList()[0] });
                                    db.Add(dockingStations[0], true);
                                });
                                ven.Add(venue.ToList()[0]);
                            }
                        }
                    }
                }
            }
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

        private async Task<bool> addUser(string stdNo, string password)
        {
            RhodeITSmartContract = new RhodeITSmartContract();
            Tuple<bool, string> added = await RhodeITSmartContract.RegisterStudent(stdNo, password);
            Console.WriteLine("Logged in: ", added);
            return added.Item1;
        }
        #endregion
    }
}
