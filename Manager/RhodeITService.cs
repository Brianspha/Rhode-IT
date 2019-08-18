//-----------------------------------------
//RhodeITService.cs
//-----------------------------------------
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using RhodeIT.Services.RhodeIT.ContractDefinition;
using RhodeIT.Helpers;
using Nethereum.Quorum;

namespace Manager
{
    public partial class RhodeITService
    {


        protected Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public RhodeITService(Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }
        public  Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Web3 web3, RhodeITDeployment rhodeITDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<RhodeITDeployment>().SendRequestAndWaitForReceiptAsync(rhodeITDeployment, cancellationTokenSource);
        }

        public  Task<string> DeployContractAsync(Web3 web3, RhodeITDeployment rhodeITDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<RhodeITDeployment>().SendRequestAsync(rhodeITDeployment);
        }

        public  async Task<RhodeITService> DeployContractAndGetServiceAsync(Web3 web3, RhodeITDeployment rhodeITDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, rhodeITDeployment, cancellationTokenSource);
            return new RhodeITService(web3, receipt.ContractAddress);
        }
        public Task<string> AddUserRequestAsync(AddUserFunction addUserFunction)
        {
            return ContractHandler.SendRequestAsync(addUserFunction);
        }

        public Task<TransactionReceipt> AddUserRequestAndWaitForReceiptAsync(AddUserFunction addUserFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addUserFunction, cancellationToken);
        }

        public Task<string> AddUserRequestAsync(string studentNo)
        {
            var addUserFunction = new AddUserFunction();
            addUserFunction.StudentNo = studentNo;
            addUserFunction.FromAddress = Vars.senderAddress;
            return ContractHandler.SendRequestAsync(addUserFunction);
        }

        public Task<TransactionReceipt> AddUserRequestAndWaitForReceiptAsync(string studentNo, CancellationTokenSource cancellationToken = null)
        {
            var addUserFunction = new AddUserFunction();
            addUserFunction.StudentNo = studentNo;
            addUserFunction.FromAddress = Vars.senderAddress;
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addUserFunction, cancellationToken);
        }

        public Task<string> RegisterDockingStationRequestAsync(RegisterDockingStationFunction registerDockingStationFunction)
        {
            return ContractHandler.SendRequestAsync(registerDockingStationFunction);
        }

        public Task<TransactionReceipt> RegisterDockingStationRequestAndWaitForReceiptAsync(RegisterDockingStationFunction registerDockingStationFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(registerDockingStationFunction, cancellationToken);
        }

        public async Task<string> RegisterDockingStationRequestAsync(string name, string latitude, string longitude)
        {
            var registerDockingStationFunction = new RegisterDockingStationFunction();
            registerDockingStationFunction.Name = name;
            registerDockingStationFunction.Latitude = latitude;
            registerDockingStationFunction.Longitude = longitude;
            registerDockingStationFunction.FromAddress = Vars.senderAddress;
            return await ContractHandler.SendRequestAsync(registerDockingStationFunction);
        }

        public Task<string> DockingStationExistsRequestAsync(DockingStationExistsFunction dockingStationExistsFunction)
        {
            return ContractHandler.SendRequestAsync(dockingStationExistsFunction);
        }

        public Task<TransactionReceipt> DockingStationExistsRequestAndWaitForReceiptAsync(DockingStationExistsFunction dockingStationExistsFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(dockingStationExistsFunction, cancellationToken);
        }

        public Task<string> DockingStationExistsRequestAsync(string name, string latitude, string longitude)
        {
            var dockingStationExistsFunction = new DockingStationExistsFunction();
            dockingStationExistsFunction.Name = name;
            dockingStationExistsFunction.Latitude = latitude;
            dockingStationExistsFunction.Longitude = longitude;
            dockingStationExistsFunction.FromAddress = Vars.senderAddress;
            return ContractHandler.SendRequestAsync(dockingStationExistsFunction);
        }

        public Task<TransactionReceipt> DockingStationExistsRequestAndWaitForReceiptAsync(string name, string latitude, string longitude, CancellationTokenSource cancellationToken = null)
        {
            var dockingStationExistsFunction = new DockingStationExistsFunction();
            dockingStationExistsFunction.Name = name;
            dockingStationExistsFunction.Latitude = latitude;
            dockingStationExistsFunction.Longitude = longitude;
            dockingStationExistsFunction.FromAddress = Vars.senderAddress;
            return ContractHandler.SendRequestAndWaitForReceiptAsync(dockingStationExistsFunction, cancellationToken);
        }

        public Task<string> UserExistsRequestAsync(UserExistsFunction userExistsFunction)
        {
            return ContractHandler.SendRequestAsync(userExistsFunction);
        }

        public Task<TransactionReceipt> UserExistsRequestAndWaitForReceiptAsync(UserExistsFunction userExistsFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(userExistsFunction, cancellationToken);
        }

        public Task<string> UserExistsRequestAsync(string studentNo)
        {
            var userExistsFunction = new UserExistsFunction();
            userExistsFunction.StudentNo = studentNo;
            userExistsFunction.FromAddress = Vars.senderAddress;
            return ContractHandler.SendRequestAsync(userExistsFunction);
        }

        public Task<TransactionReceipt> UserExistsRequestAndWaitForReceiptAsync(string studentNo, CancellationTokenSource cancellationToken = null)
        {
            var userExistsFunction = new UserExistsFunction();
            userExistsFunction.StudentNo = studentNo;
            userExistsFunction.FromAddress = Vars.senderAddress;
            return ContractHandler.SendRequestAndWaitForReceiptAsync(userExistsFunction, cancellationToken);
        }
    }
}