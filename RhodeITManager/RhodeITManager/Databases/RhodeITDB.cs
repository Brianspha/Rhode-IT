using Realms;
using Realms.Sync;
using RhodeITManager;
using RhodeITManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RhodeITManager
{
    public class RhodeITDB
    {
        Realm db;
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
            finally
            {
            }
        }     
     
        /// <summary>
        /// @dev gets all venues on campus stored locally
        /// @notice function is mainly called by admin
        /// </summary>
        /// <returns>List<VenueLocation></returns>
        public ObservableCollection<EditableVenueLocation> GetAllVenueLocations()
        {
            ObservableCollection<EditableVenueLocation> temp = new ObservableCollection<EditableVenueLocation>();
            var venues = db.All<VenueLocation>().ToList().GroupBy(item => new { item.Name }).Select(item => item.First()).OrderBy(item => item.Name);
            foreach(var v in venues)
            {
                temp.Add(new EditableVenueLocation { Name=v.Name,Long=v.Long,Lat=v.Lat,Description=v.Description,MakeDockingStation=false});
            }
            return temp;
        }
        /// <summary>
        /// @dev looks for venue using name returns the Latitude and Longitude position of venue 
        /// </summary>
        /// <param name="name">Name of the venue we look for</param>
        /// <returns></returns>
        public Point getDockingStationLatLong(string name)
        {
            var venue = db.All<VenueLocation>().Where(v => v.Name == name);
            return new Point(venue.ToList()[0].Lat, venue.ToList()[0].Long);
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
                foreach (var location in locations)
                {
                    db.Add(location, true);
                }
            });
        }


    }
}