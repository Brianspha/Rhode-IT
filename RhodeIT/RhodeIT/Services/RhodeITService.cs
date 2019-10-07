//-----------------------------------------
//RhodeITService.cs
//-----------------------------------------
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Web3;
using RhodeIT.Helpers;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RhodeIT.Services.RhodeIT
{
    public partial class RhodeITService
    {


        protected Web3 Web3 { get; }

        public Contract Contract { get; }
        public ContractHandler ContractHandler { get; }

        public RhodeITService()
        {
            Web3 = new Web3(Variables.RPCAddressNodeGenesis);
            Contract = Web3.Eth.GetContract(Variables.ABI, Variables.ContractAddress);
            ContractHandler = Web3.Eth.GetContractHandler(Variables.ContractAddress);
        }

        public async Task<Tuple<bool, string>> AddUserRequestAsync(string stud_no,string user_ethereum_address)
        {
            bool exists = await UserExistsAsync(user_ethereum_address).ConfigureAwait(false);
            Function addUserFunction = Contract.GetFunction("addUser");
            if (exists)
            {
                return new Tuple<bool, string>(true, "0x");
            }
            else
            {
                string reciept = await addUserFunction.SendTransactionAsync(Variables.senderAddress, Variables.gas, null, new object[] { stud_no }).ConfigureAwait(false);
                exists = await UserExistsAsync(user_ethereum_address).ConfigureAwait(false);
                return new Tuple<bool, string>(exists, reciept);
            }
        }
        public async Task<bool> UserExistsAsync(string user_eth_address)
        {
            Function userExistsFunction = Contract.GetFunction("userExists");
            bool exists = await userExistsFunction.CallDeserializingToObjectAsync<bool>(user_eth_address,Variables.gas,0,null).ConfigureAwait(false);
            return exists;
        }





        public async Task<bool> DockingStationExistsRequestAsync(string name, string latitude, string longitude)
        {
            Function dockingStationExistsFunction = Contract.GetFunction("dockingStationExists");
            Nethereum.Hex.HexTypes.HexBigInteger txCount = await Web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(Variables.senderAddress).ConfigureAwait(false);
            System.Numerics.BigInteger count = txCount.Value;
            bool results = await dockingStationExistsFunction.CallAsync<bool>(new object[] { name, latitude, longitude });
            return results;
        }

        public Task<bool> UserExistsRequestAndWaitForReceiptAsync(string studentNo)
        {
            Function userExistsFunction = Contract.GetFunction("userExists");
            Task<bool> results = userExistsFunction.CallAsync<bool>(new object[] { studentNo });
            return results;
        }

        public async Task<ObservableCollection<DockingStaion>> GetDockingStations()
        {
            ObservableCollection<DockingStaion> temp = new ObservableCollection<DockingStaion>();
            Function getRegisteredDockingStationKeysFunction = Contract.GetFunction("getRegisteredDockingStationKeys");
            Nethereum.Hex.HexTypes.HexBigInteger txCount = await Web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(Variables.senderAddress).ConfigureAwait(false);
            GetRegisteredDockingStationKeysOutputDTO keysReturnedValue = await getRegisteredDockingStationKeysFunction.CallDeserializingToObjectAsync<GetRegisteredDockingStationKeysOutputDTO>().ConfigureAwait(false);
            List<string> keys = keysReturnedValue.ReturnValue1;
            Function getDockingStationFunction = Contract.GetFunction("getDockingStation");
            foreach (string key in keys)
            {

                Task<GetDockingStationOutputDTO> dockingStationReturnedValue = getDockingStationFunction.CallDeserializingToObjectAsync<GetDockingStationOutputDTO>(new object[] { key });
                GetDockingStationOutputDTO dockingStationDetails = dockingStationReturnedValue.Result;
                DockingStaion dockingStation = new DockingStaion { DockingStationInformation = new VenueLocation { Name = dockingStationDetails.Name, Latitude = double.Parse(dockingStationDetails.Latitude, System.Globalization.CultureInfo.InvariantCulture), Longitude = double.Parse(dockingStationDetails.Longitude, System.Globalization.CultureInfo.InvariantCulture) } };
                temp.Add(dockingStation);
            }
            return temp;
        }
        public ObservableCollection<Bicycle> GetAvailableBicyclesFromDockingStation(string name)
        {
            ObservableCollection<Bicycle> bicycles = new ObservableCollection<Bicycle>();
            for (int i = 0; i < 5; i++)
            {
                bicycles.Add(new Bicycle { ID = new Random().Next(0, i * 3), BikeName = name, Status = false, Model = i.ToString() });
            }
            return bicycles;
        }
    }
}