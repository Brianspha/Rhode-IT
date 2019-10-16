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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RhodeIT.Services.RhodeIT
{
    public partial class RhodeITService
    {
        protected Web3 Web3 { get; }

        public Contract Contract { get; }
        public ContractHandler ContractHandler { get; }
        private HttpClient Client;
        public RhodeITService()
        {
            Web3 = new Web3(Variables.RPCAddressNodeGenesis);
            Contract = Web3.Eth.GetContract(Variables.ABI, Variables.ContractAddress);
            ContractHandler = Web3.Eth.GetContractHandler(Variables.ContractAddress);
            Client = new HttpClient();
        }

        public async Task<Tuple<bool, string>> AddUserRequestAsync(LoginDetails details)
        {
            RhodeITDB db = new RhodeITDB();
            AddUserFunction addUserFunction = new AddUserFunction
            {
                FromAddress = details.Ethereum_Address,
                Gas = Variables.gas,
                Studentno_staff_no = details.User_ID
            };
            string receipt = "0x";
            bool exists = await UserExistsRequestAndWaitForReceiptAsync(details.Ethereum_Address).ConfigureAwait(false);
            if (exists)
            {
                db.StoreTransactionReceipt(new Models.TransactionReceipt { Receipt = receipt, Activity = "LoggedIn" });
            }
            else
            {
                receipt = await ContractHandler.SendRequestAsync(addUserFunction).ConfigureAwait(false);
                exists = await UserExistsRequestAndWaitForReceiptAsync(details.Ethereum_Address).ConfigureAwait(false);
                db.StoreTransactionReceipt(new Models.TransactionReceipt { Receipt = receipt, Activity = "Registered" });
                db.UpdateLoginDetails(details);
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
        public async Task<string> GetUsercreditQueryAsync(string address)
        {
            GetUsercreditFunction getUsercreditFunction = new GetUsercreditFunction
            {
                FromAddress = address,
                Gas = Variables.gas
            };
            BigInteger balance = 0;
            try
            {
                GetUsercreditOutputDTO balanceOutput = await ContractHandler.QueryDeserializingToObjectAsync<GetUsercreditFunction, GetUsercreditOutputDTO>(getUsercreditFunction, null).ConfigureAwait(false);
                balance = (balanceOutput.ReturnValue1);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Problem with convert BigInt to int return 0 this is a bug");
                Console.WriteLine("Error: " + e.Message);
            }
            return balance.ToString();
        }
        public async Task<string> UpdateCreditRequestAsync(string address, int amount)
        {
            RhodeITDB db = new RhodeITDB();
            //@dev only admin is allowed to approve the purchasing of ride credits
            UpdateCreditFunction updateCreditFunction = new UpdateCreditFunction
            {
                FromAddress = Variables.adminAddress,
                Gas = Variables.gas,
                AmountToSend = amount,
                Receipient = address

            };
            string receipt = await ContractHandler.SendRequestAsync(updateCreditFunction).ConfigureAwait(false);
            db.StoreTransactionReceipt(new Models.TransactionReceipt { Receipt = receipt, Activity = "Purchased Ride Credits" });
            return receipt;
        }
        public async Task<bool> RentBicycleRequestAndWaitForReceiptAsync(Bicycle rental)
        {
            RhodeITDB db = new RhodeITDB();
            bool rentalResults = false;
            RentBicycleFunction rentBicycleFunction = new RentBicycleFunction
            {
                BicycleId = rental.ID,
                DockingStation = rental.DockdeAt,
                FromAddress = rental.renter,
                Gas = Variables.gas
            };
            try
            {
                Nethereum.RPC.Eth.DTOs.TransactionReceipt transactionReceipt = await ContractHandler.SendRequestAndWaitForReceiptAsync(rentBicycleFunction, null).ConfigureAwait(false);
                db.StoreTransactionReceipt(new Models.TransactionReceipt { Receipt = transactionReceipt.TransactionHash, Activity = "Rented out bicycle" });
                db.StoreUserRide(new Ride { ID = rental.ID, StationName = rental.DockdeAt, Docked = rental.Status, TransactionReceipt = transactionReceipt.TransactionHash });
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
                ObservableCollection<Bicycle> availableBicycles = await GetAvailableBicyclesFromDockingStationAsync(key).ConfigureAwait(false);
                DockingStaion dockingStation = new DockingStaion { DockingStationInformation = new VenueLocation { Name = dockingStationDetails.ReturnValue1, Latitude = double.Parse(dockingStationDetails.ReturnValue2, System.Globalization.CultureInfo.InvariantCulture), Longitude = double.Parse(dockingStationDetails.ReturnValue3, System.Globalization.CultureInfo.InvariantCulture) } };
                foreach (Bicycle bicycle in availableBicycles)
                {
                    dockingStation.AvailableBicycles.Add(bicycle);
                }
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
                    bicycles.Add(new Bicycle { ID = key, Status = bicycleDetails.ReturnValue2 == true ? "Available" : "Not Available", DockdeAt = bicycleDetails.ReturnValue1 });
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
        public async Task<int> GetCurrentRideCostQueryAsync()
        {
            GetCurrentRideCostFunction getCurrentRideCostFunction = new GetCurrentRideCostFunction
            {
                Gas = Variables.gas,
                FromAddress = Variables.adminAddress
            };
            int cost = await ContractHandler.QueryAsync<GetCurrentRideCostFunction, int>(getCurrentRideCostFunction, null).ConfigureAwait(false);
            return cost;
        }
    }

}