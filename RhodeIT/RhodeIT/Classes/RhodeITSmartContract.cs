using Nethereum.Quorum;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts.Managed;
using RhodeIT.Databases;
using RhodeIT.Helpers;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using RhodeIT.Services.RhodeIT.ContractDefinition;
using System;

namespace RhodeIT.Classes
{
    /// <summary>
    /// responsible for registering a student on the smartcontract and updating the user login on the Postgres server 
    /// </summary>
    public sealed class RhodeITSmartContract
    {
        private RhodeITDB rhodeITDB;
        private RhodesDataBase rhodesDataBase;
        public Web3Quorum web3 { get; set; }
        public TransactionReceipt Receipt { get; private set; }
        public RhodeITService SmartContractFunctions { get; private set; }

        private RhodeITDeployment BaseContract;
        private ManagedAccount Account;
        public RhodeITSmartContract()
        {
            SetupAsync();
        }
        /// <summary>
        /// @dev responsible for setting up connection to the following
        /// @var rhodeITDB the RhodesIT Platforms Database
        /// @var rhodesDataBase represents Rhodes Universities DB
        /// @var web3 responsible for establishing a connection between RChain (Private Blockchain) and the Mobile App
        /// @var SmartContractFunctions responsible for allowing access to all smart contract functions
        /// </summary>
        public void SetupAsync()
        {
            BaseContract = new RhodeITDeployment();
            rhodeITDB = new RhodeITDB();
            rhodesDataBase = new RhodesDataBase();
            Account = new ManagedAccount(Variables.adminAddress, Variables.Passwordd);
            web3 = new Web3Quorum(Variables.RPCAddressNodeGenesis);
            web3.TransactionManager.DefaultGas = new System.Numerics.BigInteger(8000000);
            web3.TransactionManager.DefaultGasPrice = new System.Numerics.BigInteger(20000000000);
            SmartContractFunctions = new RhodeITService();
        }
        /// <summary>
        /// @dev function that allows user to login this function also adds users to the smartcontracts storage users dont not need to register university credentials will be sufficient
        /// </summary>
        /// <param name="studentNo">student number</param>
        /// <param name="password">password</param>
        /// <returns>if the login was succefull or not</returns>
        public async void RegisterStudent(LoginDetails details)
        {
            details.User_ID = details.User_ID.ToLower();
            string Thash = "";
            Tuple<bool, string> results = await SmartContractFunctions.AddUserRequestAsync(details);
            Thash = results.Item2;
            details.TransactionHash = Thash;
            rhodeITDB.UpdateLoginDetails(details);
        }

    }
}
