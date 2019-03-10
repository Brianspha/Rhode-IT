using Realms;
using Realms.Sync;
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
        public void StoreVenueLocations(List<VenueLocation> locs)
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
    }
}
