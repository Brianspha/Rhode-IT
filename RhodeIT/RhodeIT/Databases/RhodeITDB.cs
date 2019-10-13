using Plugin.SecureStorage;
using Realms;
using RhodeIT.Classes;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RhodeIT.Databases
{
    public class RhodeITDB
    {
        private Realm db;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RealmDataBase"/> class.
        /// </summary>
        public RhodeITDB()
        {
        }

        private void SetUpDB()
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
            SetUpDB();
            List<DockingStaion> temp = db.All<DockingStaion>().ToList();
            return temp.Count == 0 ? true : false;
        }
        /// <summary>
        /// @dev checks if the app was launched for the first time by an admin
        /// </summary>
        /// <returns>bool </returns>
        public bool FirstRunAdmin()
        {
            SetUpDB();
            List<VenueLocation> temp = db.All<VenueLocation>().ToList();
            return temp.Count == 0 ? true : false;
        }

        /// <summary>
        /// Updates the users login details
        /// </summary>
        /// <param name="dets">login credentials of user</param>
        public void UpdateLoginDetails(LoginDetails dets)
        {
            SetUpDB();
            CrossSecureStorage.Current.SetValue("Password", dets.Password);
            CrossSecureStorage.Current.SetValue("ID", dets.User_ID);
            CrossSecureStorage.Current.SetValue("Credit", dets.RideCredits.ToString());
            CrossSecureStorage.Current.SetValue("Address", dets.Ethereum_Address);
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
        public LoginDetails GetUserDetails()
        {
            SetUpDB();
            LoginDetails temp = new LoginDetails();
            try
            {
                temp = new LoginDetails { User_ID = CrossSecureStorage.Current.GetValue("ID"), Password = CrossSecureStorage.Current.GetValue("Password"), Ethereum_Address = CrossSecureStorage.Current.GetValue("Address"), RideCredits = Convert.ToInt32(CrossSecureStorage.Current.GetValue("Credit")) };
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
        public void LogOut()
        {
            SetUpDB();
            CrossSecureStorage.Current.DeleteKey("Password");
            CrossSecureStorage.Current.DeleteKey("ID");
            Console.WriteLine("Logging out");

        }

        /// <summary>
        /// @dev stores all venues passed into the function
        /// </summary>
        /// <param name="locations"> names of the docking stations</param>
        public void StoreVenueLocations(List<VenueLocation> locations)
        {
            SetUpDB();
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

        public void StoreTransactionReceipt(TransactionReciept reciept)
        {
            SetUpDB();
            db.Write(() =>
            {
                db.Add(reciept, true);
            });
        }

        public List<TransactionReciept> GetTransactionReceipts()
        {
            SetUpDB();
            return db.All<TransactionReciept>().ToList();
        }
    }
}
