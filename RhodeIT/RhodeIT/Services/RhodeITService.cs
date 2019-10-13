//-----------------------------------------
//RhodeITService.cs
//-----------------------------------------
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Newtonsoft.Json.Linq;
using RhodeIT.Databases;
using RhodeIT.Helpers;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RhodeIT.Services.RhodeIT
{
    public partial class RhodeITService
    {
        protected Web3 Web3 { get; }

        public Contract Contract { get; }
        public ContractHandler ContractHandler { get; }
        public RhodeITDB db;
        private HttpClient Client;
        public RhodeITService()
        {
            Web3 = new Web3(Variables.RPCAddressNodeGenesis);
            Contract = Web3.Eth.GetContract(Variables.ABI, Variables.ContractAddress);
            ContractHandler = Web3.Eth.GetContractHandler(Variables.ContractAddress);
            db = new RhodeITDB();
            Client = new HttpClient();
        }

        public async Task<Tuple<bool, string>> AddUserRequestAsync(LoginDetails details)
        {
            AddUserFunction addUserFunction = new AddUserFunction
            {
                FromAddress = details.Ethereum_Address,
                Gas = Variables.gas,
                // GasPrice = Variables.gasPrice,
                Studentno_staff_no = details.User_ID
            };
            string receipt = "";
            bool exists = await UserExistsRequestAndWaitForReceiptAsync(details.Ethereum_Address).ConfigureAwait(false);
            if (exists)
            {
                db.StoreTransactionReceipt(new TransactionReciept { Receipt = "0x", Activity = "LoggedIn" });
            }
            else
            {
                receipt = await ContractHandler.SendRequestAsync(addUserFunction).ConfigureAwait(false);
                exists = await UserExistsRequestAndWaitForReceiptAsync(details.Ethereum_Address).ConfigureAwait(false);
                db.StoreTransactionReceipt(new TransactionReciept { Receipt = receipt, Activity = "Registered" });
            }
            return new Tuple<bool, string>(exists, receipt);
        }
        public async Task<bool> UserExistsRequestAndWaitForReceiptAsync(string address)
        {
            UserExistsFunction userExistsFunction = new UserExistsFunction
            {
                FromAddress = address,
                Gas = Variables.gas,
                //  GasPrice = Variables.gasPrice
            };
            bool results = await ContractHandler.QueryAsync<UserExistsFunction, bool>(userExistsFunction, null).ConfigureAwait(false);
            return results;
        }
        public async Task<int> GetUsercreditQueryAsync(string address)
        {
            GetUsercreditFunction getUsercreditFunction = new GetUsercreditFunction
            {
                FromAddress = address,
                Gas = Variables.gas,
                ///    GasPrice = Variables.gasPrice
            };
            int balance = 0;
            try
            {
                balance = await ContractHandler.QueryAsync<GetUsercreditFunction, int>(getUsercreditFunction, null).ConfigureAwait(false);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Problem with convert BigInt to int return 0 this is a bug");
            }
            return balance;
        }
        public async Task<string> UpdateCreditRequestAsync(string address, int amount)
        {
            //@dev only admin is allowed to approve the purchasing of ride credits
            UpdateCreditFunction updateCreditFunction = new UpdateCreditFunction
            {
                FromAddress = Variables.adminAddress,
                Gas = Variables.gas,
                AmountToSend = amount,
                Receipient=address
                
            };
            string receipt = await ContractHandler.SendRequestAsync(updateCreditFunction).ConfigureAwait(false);
            db.StoreTransactionReceipt(new TransactionReciept { Receipt = receipt, Activity = "Purchased Ride Credits" });
            return receipt;
        }
        public async Task<bool> RentBicycleRequestAndWaitForReceiptAsync(Bicycle rental)
        {
            bool rentalResults = false;
            RentBicycleFunction rentBicycleFunction = new RentBicycleFunction
            {
                BicycleId = rental.ID,
                DockingStation = rental.DockdeAt,
                FromAddress = rental.renter,
                Gas = Variables.gas,
                ///      GasPrice = Variables.gasPrice,
                ///   
            };
            try
            {
                TransactionReceipt transactionReceipt = await ContractHandler.SendRequestAndWaitForReceiptAsync(rentBicycleFunction, null).ConfigureAwait(false);
                db.StoreTransactionReceipt(new TransactionReciept { Receipt = transactionReceipt.TransactionHash, Activity = "Rented out bicycle" });
                rentalResults = await UnlockBicycleAsync(rental).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong bicycle rental: " + e.Message);
                return rentalResults;
            }
            return rentalResults;
        }
        public async Task<ObservableCollection<DockingStaion>> GetDockingStations()
        {
            ObservableCollection<DockingStaion> temp = new ObservableCollection<DockingStaion>();
            List<string> keys = await GetRegisteredDockingStationKeysQueryAsync().ConfigureAwait(false);
            foreach (string key in keys)
            {
                GetDockingStationOutputDTO dockingStationDetails = await GetDockingStationQueryAsync(key).ConfigureAwait(false);
                DockingStaion dockingStation = new DockingStaion { DockingStationInformation = new VenueLocation { Name = dockingStationDetails.Name, Latitude = double.Parse(dockingStationDetails.Latitude, System.Globalization.CultureInfo.InvariantCulture), Longitude = double.Parse(dockingStationDetails.Longitude, System.Globalization.CultureInfo.InvariantCulture) } };
                temp.Add(dockingStation);
            }
            return temp;
        }

        public async Task<GetDockingStationOutputDTO> GetDockingStationQueryAsync(string stationName)
        {
            LoginDetails details = new RhodeITDB().GetUserDetails();
            GetDockingStationFunction getDockingStationFunction = new GetDockingStationFunction
            {
                StationName = stationName,
                FromAddress = details.Ethereum_Address,
                Gas = Variables.gas
            };
            GetDockingStationOutputDTO stationDetails = await ContractHandler.QueryDeserializingToObjectAsync<GetDockingStationFunction, GetDockingStationOutputDTO>(getDockingStationFunction).ConfigureAwait(false);
            return stationDetails;
        }
        public async Task<List<string>> GetRegisteredDockingStationKeysQueryAsync()
        {
            LoginDetails details = new RhodeITDB().GetUserDetails();
            GetRegisteredDockingStationKeysFunction getRegisteredDockingStationKeysFunction = new GetRegisteredDockingStationKeysFunction
            {
                FromAddress = details.Ethereum_Address,
                Gas = Variables.gas
            };
            List<string> keys = await ContractHandler.QueryAsync<GetRegisteredDockingStationKeysFunction, List<string>>(getRegisteredDockingStationKeysFunction, null).ConfigureAwait(false);
            return keys;
        }

        public async Task<GetDockingStationOutputDTO> GetDockingStationQueryAsync()
        {
            LoginDetails details = new RhodeITDB().GetUserDetails();
            GetDockingStationFunction getDockingStationFunction = new GetDockingStationFunction
            {
                Gas = Variables.gas,
                FromAddress = details.Ethereum_Address
            };
            GetDockingStationOutputDTO stationDetails = await ContractHandler.QueryDeserializingToObjectAsync<GetDockingStationFunction, GetDockingStationOutputDTO>(getDockingStationFunction, null).ConfigureAwait(false);
            return stationDetails;
        }

        public async Task<List<string>> GetBicycleKeys()
        {
            GetRegisteredBicycleKeysFunction getBicycleKeysFunction = new GetRegisteredBicycleKeysFunction
            {
                FromAddress = Variables.adminAddress,
                Gas = Variables.gas
            };
            GetRegisteredBicycleKeysOutputDTO bicycleKeys = await ContractHandler.QueryDeserializingToObjectAsync<GetRegisteredBicycleKeysFunction, GetRegisteredBicycleKeysOutputDTO>(getBicycleKeysFunction, null).ConfigureAwait(false);
            return bicycleKeys.ReturnValue1;
        }
        public async Task<GetBicycleOutputDTO> GetBicycleInformation(string bikeId)
        {
            GetBicycleFunction bicycleFunction = new GetBicycleFunction
            {
                FromAddress = Variables.adminAddress,
                Gas = Variables.gas,
                BicycleId = bikeId
            };
            GetBicycleOutputDTO details = await ContractHandler.QueryDeserializingToObjectAsync<GetBicycleFunction, GetBicycleOutputDTO>(bicycleFunction, null).ConfigureAwait(false);
            return details;
        }
        public async Task<ObservableCollection<Bicycle>> GetAvailableBicyclesFromDockingStationAsync(string dockingStation)
        {
            ObservableCollection<Bicycle> bicycles = new ObservableCollection<Bicycle>();
            List<string> keys = await GetBicycleKeys().ConfigureAwait(false);
            foreach (string key in keys)
            {
                GetBicycleOutputDTO bicycleDetails = await GetBicycleInformation(key).ConfigureAwait(false);
                if (bicycleDetails.ReturnValue2)
                {
                    bicycles.Add(new Bicycle { ID = key, Status = bicycleDetails.ReturnValue2 ==true? "Available":"Not Available", DockdeAt = bicycleDetails.ReturnValue1 });
                }
            }
            return bicycles;
        }
        public async Task<bool> UnlockBicycleAsync(Bicycle bicycle)
        {
            bool success = false;
            string sUrl = Variables.rentBicycle;
            string sContentType = "application/json"; // or application/xml

            JObject rental = new JObject
            {
                { "eth_address", bicycle.renter },
                { "bikeId", bicycle.ID },
                { "dockingStation", bicycle.DockdeAt }
            };

            HttpClient oHttpClient = new HttpClient();
            HttpResponseMessage results = await oHttpClient.PutAsync(sUrl, new StringContent(rental.ToString(), Encoding.UTF8, sContentType)).ConfigureAwait(false);
            if (results.StatusCode != HttpStatusCode.NotFound)
            {
                success = true;
                ///HttpContentHeaders response = results.Content.Headers;
            }
            return success;
        }
        public Task<bool> DockBicycle(Bicycle bicycle)
        {
            bool success = false;
            string sUrl = Variables.dockBicycle;
            string sContentType = "application/json"; // or application/xml

            JObject dock = new JObject
            {
                { "eth_address", bicycle.renter },
                { "bikeId", bicycle.ID },
                { "dockingStation", bicycle.DockdeAt }
            };

            HttpClient oHttpClient = new HttpClient();
            Task<HttpResponseMessage> oTaskPostAsync = oHttpClient.PutAsync(sUrl, new StringContent(dock.ToString(), Encoding.UTF8, sContentType));
            oTaskPostAsync.ContinueWith((oHttpResponseMessage) =>
            {
                HttpResponseMessage results = oHttpResponseMessage.Result;
                if (results.StatusCode != HttpStatusCode.NotFound)
                {
                    success = true;
                    ///HttpContentHeaders response = results.Content.Headers;
                }

            });
            return Task.FromResult(success);
        }
    }

}