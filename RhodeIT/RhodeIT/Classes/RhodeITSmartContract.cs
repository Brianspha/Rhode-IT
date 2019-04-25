using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Newtonsoft.Json.Linq;
using RhodeIT.Databases;
using RhodeIT.Helpers;
using RhodeIT.Models;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;

namespace RhodeIT.Classes
{
    /// <summary>
    /// @dev responsible for communicating with RhodeIT smart contract
    /// </summary>
    public sealed class RhodeITSmartContract
    {
        LocalDataBase db;
        public string senderAddress = "0x9B0C03F88c8e1b266eCcB7f8Ac09BF1fC4EBbb9d";
        public string password = "password";
        public string abi = @"[{'constant':false,'inputs':[{'name':'studentNo','type':'string'},{'name':'pass','type':'string'}],'name':'addUser','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'name','type':'string'},{'name':'latitude','type':'string'},{'name':'longitude','type':'string'}],'name':'registerDockingStation','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'name','type':'string'}],'name':'DockingStationExists','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'studentNo','type':'string'}],'name':'userExists','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'inputs':[],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'name':'userID','type':'string'},{'indexed':true,'name':'results','type':'bool'}],'name':'addUserLogger','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'userID','type':'string'},{'indexed':true,'name':'results','type':'bool'}],'name':'userExistsLogger','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'name','type':'string'},{'indexed':true,'name':'Latitude','type':'string'},{'indexed':true,'name':'Longitude','type':'string'},{'indexed':false,'name':'results','type':'bool'}],'name':'addDockingStationLogger','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'name','type':'string'},{'indexed':true,'name':'results','type':'bool'}],'name':'dockingStationExistsLogger','type':'event'}]";
        public string byteCode = @"0x60806040526000600255600060035534801561001a57600080fd5b5033151561002757600080fd5b60048054600160a060020a03191633179055610be9806100486000396000f3006080604052600436106100615763ffffffff7c0100000000000000000000000000000000000000000000000000000000600035041663079eaf348114610066578063ab84f85314610111578063ef416216146101e6578063fddc968e1461023f575b600080fd5b34801561007257600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100fd94369492936024939284019190819084018382808284375050604080516020601f89358b018035918201839004830284018301909452808352979a9998810197919650918201945092508291508401838280828437509497506102989650505050505050565b604080519115158252519081900360200190f35b34801561011d57600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100fd94369492936024939284019190819084018382808284375050604080516020601f89358b018035918201839004830284018301909452808352979a99988101979196509182019450925082915084018382808284375050604080516020601f89358b018035918201839004830284018301909452808352979a9998810197919650918201945092508291508401838280828437509497506104879650505050505050565b3480156101f257600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100fd9436949293602493928401919081908401838280828437509497506107719650505050505050565b34801561024b57600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100fd9436949293602493928401919081908401838280828437509497506109219650505050505050565b6000331515610317576040805160e560020a62461bcd02815260206004820152602a60248201527f496e76616c69642073656e646572206164647265737320696e2061646455736560448201527f722066756e6374696f6e00000000000000000000000000000000000000000000606482015290519081900360840190fd5b60806040519081016040528084815260200183815260200160008152602001600115158152506000846040518082805190602001908083835b6020831061036f5780518252601f199092019160209182019101610350565b51815160209384036101000a60001901801990921691161790529201948552506040519384900381019093208451805191946103b094508593500190610af7565b5060208281015180516103c99260018501920190610af7565b5060408281015160028301556060909201516003909101805460ff19169115159190911790555183516001918591819060208401908083835b602083106104215780518252601f199092019160209182019101610402565b5181516020939093036101000a60001901801990911692169190911790526040519201829003822093507f510c21937573aecd1b0ad7bdd2882f42a405fd8acdfdc54c19e7055fe857406e92506000919050a35060038054600190810190915592915050565b6000610491610b75565b3315156104e8576040805160e560020a62461bcd02815260206004820152601660248201527f496e76616c69642073656e646572206164647265737300000000000000000000604482015290519081900360640190fd5b60806040519081016040528086815260200185815260200184815260200160011515815250905060606040519081016040528082815260200160008152602001600115158152506001866040518082805190602001908083835b602083106105615780518252601f199092019160209182019101610542565b51815160209384036101000a600019018019909216911617905292019485525060405193849003810190932084518051805192959194508593506105ab9284929190910190610af7565b5060208281015180516105c49260018501920190610af7565b50604082015180516105e0916002840191602090910190610af7565b50606091909101516003909101805491151560ff19928316179055602083810151600484015560409384015160069093018054931515939092169290921790556002805460010190559051845185928291908401908083835b602083106106585780518252601f199092019160209182019101610639565b51815160209384036101000a6000190180199092169116179052604051919093018190038120895190955089945090928392508401908083835b602083106106b15780518252601f199092019160209182019101610692565b51815160209384036101000a60001901801990921691161790526040519190930181900381208b519095508b945090928392508401908083835b6020831061070a5780518252601f1990920191602091820191016106eb565b51815160209384036101000a6000190180199092169116179052604080519290940182900382206001835293519395507f0d66aab15a6e2bf4290f8369fd0b0cde47ae4a1c417afdf99a4fb09723290a3a94509083900301919050a4506001949350505050565b60003315156107ca576040805160e560020a62461bcd02815260206004820152601660248201527f496e76616c69642073656e646572206164647265737300000000000000000000604482015290519081900360640190fd5b6001826040518082805190602001908083835b602083106107fc5780518252601f1990920191602091820191016107dd565b51815160209384036101000a6000190180199092169116179052920194855250604051938490038101842060060154865160ff909116151594879450925082918401908083835b602083106108625780518252601f199092019160209182019101610843565b5181516020939093036101000a60001901801990911692169190911790526040519201829003822093507f0a8e17507c443525dbb4f1b61bfcf3d8b410cfcd454d644d73ff945dadbc6d8f92506000919050a36001826040518082805190602001908083835b602083106108e75780518252601f1990920191602091820191016108c8565b51815160209384036101000a600019018019909216911617905292019485525060405193849003019092206006015460ff16949350505050565b60003315156109a0576040805160e560020a62461bcd02815260206004820152602a60248201527f496e76616c69642073656e646572206164647265737320696e2066756e63746960448201527f6f6e206765745573657200000000000000000000000000000000000000000000606482015290519081900360840190fd5b6000826040518082805190602001908083835b602083106109d25780518252601f1990920191602091820191016109b3565b51815160209384036101000a6000190180199092169116179052920194855250604051938490038101842060030154865160ff909116151594879450925082918401908083835b60208310610a385780518252601f199092019160209182019101610a19565b5181516020939093036101000a60001901801990911692169190911790526040519201829003822093507f2b8bf7028312094fd665dba3ac68e167e0d1bf2c8a4eae069e1bc1f722ea5fa392506000919050a36000826040518082805190602001908083835b60208310610abd5780518252601f199092019160209182019101610a9e565b51815160209384036101000a600019018019909216911617905292019485525060405193849003019092206003015460ff16949350505050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10610b3857805160ff1916838001178555610b65565b82800160010185558215610b65579182015b82811115610b65578251825591602001919060010190610b4a565b50610b71929150610ba0565b5090565b6080604051908101604052806060815260200160608152602001606081526020016000151581525090565b610bba91905b80821115610b715760008155600101610ba6565b905600a165627a7a72305820c25ec14cca1adc7bd4d9f581b02bf4a48370592e6015d93265df9416ab17dc560029";
        public Web3 web3 = new Web3("HTTP://146.231.123.144:7545");
        public TransactionReceipt Receipt { get; private set; }
        bool unlockAccountResult { get; set; }
        string transactionHash { get; set; }
        Contract Contract { get; set; }
        string contractAddress = "0x2185A68bE9F21E1dcf534420C69Bf1bCb030967E";
        HexBigInteger gasLimit { get; set; }
        HexBigInteger gasPrice { get; set; }
        HexBigInteger Ether { get; set; }
       
        static RhodeITSmartContract()
        {

        }
        public RhodeITSmartContract()
        {
            db = new LocalDataBase();
            Setup();
        }
        public void Setup()
        {
            web3.TransactionManager.DefaultGas = new System.Numerics.BigInteger(10000000);
            web3.TransactionManager.DefaultGasPrice = new System.Numerics.BigInteger(20000000000);
            Contract = web3.Eth.GetContract(abi,contractAddress);

        }
        /// <summary>
        /// @dev function that allows user to login this function also adds users to the smartcontracts storage users dont not need to register university credentials will be sufficient
        /// </summary>
        /// <param name="sn">student number</param>
        /// <param name="pass">password</param>
        /// <returns>if the login was succefull or not</returns>
        public async Task<bool> Login(string sn, string pass)
        {
            sn = sn.ToLower();
            bool added = false;
            //   var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120);
            //   unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120);
            //transactionHash = await web3.Eth.DeployContract.SendRequestAsync(byteCode, senderAddress);
            //Receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            //while (Receipt == null)
            //{
            //    Thread.Sleep(5000);
            //    Receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            //}
            var userExistsEvent = Contract.GetEvent("userExistsLogger");
            var addUser = Contract.GetFunction("addUser");
            var userExists = Contract.GetFunction("userExists");
            var exists = await userExists.CallAsync<bool>(sn);//@dev we interested in the latest event which is why we grabbing at index -1
            if (!exists)
            {
                var send = await addUser.SendTransactionAndWaitForReceiptAsync(senderAddress, default, new object[] { sn, pass });
                var addUserEventHandler = web3.Eth.GetEvent<AddUserLogger>(contractAddress);
                var filterAlladdUserForContract = addUserEventHandler.CreateFilterInput();
                var alladdUserEvents = await addUserEventHandler.GetAllChanges(filterAlladdUserForContract);
                added = alladdUserEvents[alladdUserEvents.Count - 1].Event.Results;//@dev we interested in the latest event which is why we grabbing at index -1
                Console.WriteLine("Returned from adding user: ", send.Status.ToString());
                if (added)
                {
                    added = true;
                    db.Login(new LoginDetails { userID = sn, password = password ,TransactionHash=send.TransactionHash});
                }
            }
            return added;
        }
        public async Task<bool> registerDockingStation(List<VenueLocation> venues)
        {

            bool added = false;
            foreach (var venue in venues)
            {
                Console.WriteLine("Did we add DockingStation: ", venue.Name, "Success: ", added);

                var registerDockingStation = Contract.GetFunction("registerDockingStation");
                var DockingStationExists = Contract.GetFunction("DockingStationExists");
                var dockingStationExitsEventHandler = web3.Eth.GetEvent<DockingStationExistsLogger>();
                var result = await DockingStationExists.SendTransactionAndWaitForReceiptAsync(senderAddress, default, new object[] { venue.Name });
                var filterforDockingStationExist = dockingStationExitsEventHandler.CreateFilterInput();
                var allEventsForDockingStationExists = await dockingStationExitsEventHandler.GetAllChanges(filterforDockingStationExist);
                var results = allEventsForDockingStationExists[allEventsForDockingStationExists.Count - 1];
                if (!results.Event.Results)
                {
                    var send = await registerDockingStation.SendTransactionAndWaitForReceiptAsync(senderAddress, default, new object[] { venue.Name, venue.Lat.ToString(), venue.Long.ToString() });
                    var registerDockingStationEventHandler = web3.Eth.GetEvent<AddDockingStationLogger>();
                    var filterAlladdDockingStationEventsForContract = registerDockingStationEventHandler.CreateFilterInput();
                    var alladdDockingStationsEvents = await registerDockingStationEventHandler.GetAllChanges(filterAlladdDockingStationEventsForContract);
                    added = alladdDockingStationsEvents[alladdDockingStationsEvents.Count - 1].Event.Results;
                    Console.WriteLine("Returned from adding user: ", added);
                }
            }
            return added;
        }


    }
}
