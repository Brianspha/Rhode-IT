using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using RhodeIT.Databases;
using RhodeIT.Helpers;
using RhodeIT.Models;
using Nethereum.Quorum;
using RhodeIT.Services.RhodeIT;
using RhodeIT.Services.RhodeIT.ContractDefinition;
using Nethereum.Web3.Accounts.Managed;

namespace RhodeIT.Classes
{
    /// <summary>
    /// @dev responsible for communicating with RhodeIT smart contract
    /// </summary>
    public sealed class RhodeITSmartContract
    {
        RhodeITDB rhodeITDB;
        RhodesDataBase rhodesDataBase;
        public Web3Quorum web3 { get; set; }
        public TransactionReceipt Receipt { get; private set; }
        public RhodeITService SmartContractFunctions { get; private set; }
        RhodeITDeployment BaseContract;
        ManagedAccount Account;
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
            Account = new ManagedAccount(Variables.senderAddress, Variables.Passwordd);
            web3 = new Web3Quorum(Variables.RPCAddressNodeGenesis);
            web3.TransactionManager.DefaultGas = new System.Numerics.BigInteger(8000000);
            web3.TransactionManager.DefaultGasPrice = new System.Numerics.BigInteger(20000000000);
            SmartContractFunctions = new RhodeITService();
            //var transactionHash= await SmartContractFunctions.DeployContractAsync(web3, BaseContract);
            //Console.WriteLine("Deployed Contract: " + transactionHash);
        }
        /// <summary>
        /// @dev function that allows user to login this function also adds users to the smartcontracts storage users dont not need to register university credentials will be sufficient
        /// </summary>
        /// <param name="studentNo">student number</param>
        /// <param name="password">password</param>
        /// <returns>if the login was succefull or not</returns>
        public async Task<Tuple<bool, string>> RegisterStudent(string studentNo, string password)
        {
            studentNo = studentNo.ToLower();
            string Thash = "";
            var results = await SmartContractFunctions.AddUserRequestAsync(studentNo);
            Thash = results.Item2;
            rhodeITDB.Login(new LoginDetails { userID = studentNo, password = password, TransactionHash = Thash });
            return new Tuple<bool, string>(results.Item1, results.Item2);
        }

    }
}
