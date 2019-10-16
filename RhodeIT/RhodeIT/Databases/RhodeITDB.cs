using Plugin.SecureStorage;
using Realms;
using RhodeIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RhodeIT.Databases
{
    public class RhodeITDB
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RealmDataBase"/> class.
        /// </summary>
        public RhodeITDB()
        {
        }

        private Realm SetUpDB()
        {
            Realm db = null;
            try
            {
                db = Realm.GetInstance();//open realm db
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
            return db;
        }

        /// <summary>
        /// Checks if the App has been launched before .
        /// </summary>
        /// <returns><c>true</c>, if run was firsted, <c>false</c> otherwise.</returns>
        public bool FirstRun()
        {
            Realm db = SetUpDB();
            List<DockingStaion> temp = db.All<DockingStaion>().ToList();
            return temp.Count == 0 ? true : false;
        }
        /// <summary>
        /// Checks if the app was launched for the first time by an admin
        /// </summary>
        /// <returns>bool </returns>
        public bool FirstRunAdmin()
        {
            Realm db = SetUpDB();
            List<VenueLocation> temp = db.All<VenueLocation>().ToList();
            return temp.Count == 0 ? true : false;
        }

        /// <summary>
        /// Updates the users login details
        /// </summary>
        /// <param name="dets">login credentials of user</param>
        public void UpdateLoginDetails(LoginDetails dets)
        {
            CrossSecureStorage.Current.SetValue("Password", dets.Password);
            CrossSecureStorage.Current.SetValue("ID", dets.User_ID);
            CrossSecureStorage.Current.SetValue("Credit", dets.RideCredits == null ? "" : dets.RideCredits.ToString());
            CrossSecureStorage.Current.SetValue("Address", dets.Ethereum_Address);
            Console.WriteLine("Logged in");
        }
        /// <summary>
        /// Checks to see if the user had logged in before if yes we return they credientials
        /// </summary>
        /// <returns>LoginDetails</returns>
        public LoginDetails GetUserDetails()
        {
            Realm db = SetUpDB();
            LoginDetails temp = new LoginDetails();
            try
            {
                temp = new LoginDetails { User_ID = CrossSecureStorage.Current.GetValue("ID"), Password = CrossSecureStorage.Current.GetValue("Password"), Ethereum_Address = CrossSecureStorage.Current.GetValue("Address"), RideCredits = CrossSecureStorage.Current.GetValue("Credit") };
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
        /// Deletes user login credentials everytime they loggout
        /// </summary>
        public void LogOut()
        {
            CrossSecureStorage.Current.DeleteKey("Password");
            CrossSecureStorage.Current.DeleteKey("ID");
            CrossSecureStorage.Current.DeleteKey("Ethereum_Address");
            Console.WriteLine("Logging out");
        }

        /// <summary>
        /// Stores all venues passed into the function
        /// </summary>
        /// <param name="locations"> names of the docking stations</param>
        public void StoreVenueLocations(List<VenueLocation> locations)
        {
            Realm db = SetUpDB();
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
        /// Stores all transaction receipts for user transactions
        /// </summary>
        /// <param name="reciept"></param>
        public void StoreTransactionReceipt(TransactionReceipt reciept)
        {
            Realm db = SetUpDB();
            db.Write(() =>
            {
                db.Add(reciept, true);
            });
        }
        /// <summary>
        /// Returns all transaction receipts for user transactions
        /// </summary>
        /// <returns>List<TransactionReciept></returns>
        public List<TransactionReceipt> GetTransactionReceipts()
        {
            Realm db = SetUpDB();
            List<TransactionReceipt> data = new List<TransactionReceipt>();
            db.Write(() =>
            {
                data = db.All<TransactionReceipt>().ToList();
            });
            return data;
        }
        /// <summary>
        /// Stores a users latest ride
        /// </summary>
        /// <param name="ride"></param>
        public void StoreUserRide(Ride ride)
        {
            Realm db = SetUpDB();
            db.Write(() =>
            {
                db.Add(ride, true);
            });
        }
        /// <summary>
        /// Returns all user rides
        /// </summary>
        /// <returns>List<Ride></returns>
        public List<Ride> GetUserUpComingRides()
        {
            Realm db = SetUpDB();
            List<Ride> data = new List<Ride>();

            db.Write(() =>
            {
                data = db.All<Ride>().ToList();
            });
            return data;
        }
        public void RemoveRideFromList(Ride ride)
        {
            Realm db = SetUpDB();
            db.Write(() =>
            {
                List<Ride> all = db.All<Ride>().ToList();
                List<Ride> newRides = new List<Ride>();
                for (int i = 0; i < all.Count; i++)
                {
                    if (all[i].ID == ride.ID)
                    {
                        continue;
                    }

                    newRides.Add(all[i]);
                }
                if (newRides.Count == 0)
                {
                    db.RemoveAll<Ride>();
                }
                else
                {
                    foreach (Ride tempride in newRides)
                    {
                        db.Add(tempride, true);
                    }
                }
            });
        }
    }
}
