using Plugin.SecureStorage;
using Realms;
using Realms.Sync;
using Rhode_IT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IPConfig getCurrentDockingStationIP()
        {
            var IP =db.All<IPConfig>().ToList<IPConfig>();
            return IP.ElementAt(0);
        }
    }
}
