using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum;
namespace Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            new RhodeITManager().Run().Wait();
        }
    }

    public class RhodeITManager
    {
        public async Task Run()
        {
            //Basic Authentication

            var web3 = new Web3("HTTP://146.231.123.137:11001");
            var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            web3.TransactionManager.DefaultGas = new BigInteger(8000000);
            web3.TransactionManager.DefaultGasPrice = new BigInteger(20000000000);
             var accounts = await web3.Eth.Accounts.SendRequestAsync();
            var balance = await web3.Eth.GetBalance.SendRequestAsync(accounts[0]);
            var rootAddressBalance = await web3.Eth.GetBalance.SendRequestAsync(accounts[0]);
            var test = web3.Eth.GetContract("[{'constant':false,'inputs':[{'name':'studentNo','type':'string'}],'name':'addUser','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'name','type':'string'},{'name':'latitude','type':'string'},{'name':'longitude','type':'string'}],'name':'registerDockingStation','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'name','type':'string'},{'name':'latitude','type':'string'},{'name':'longitude','type':'string'}],'name':'dockingStationExists','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'studentNo','type':'string'}],'name':'userExists','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'inputs':[],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'name':'results','type':'bool'}],'name':'addUserLogger','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'tHash','type':'bytes32'},{'indexed':true,'name':'results','type':'bool'}],'name':'userExistsLogger','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'tHash','type':'bytes32'},{'indexed':true,'name':'results','type':'bool'}],'name':'addDockingStationLogger','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'tHash','type':'bytes32'},{'indexed':true,'name':'results','type':'bool'}],'name':'dockingStationExistsLogger','type':'event'}]", "0x2ca1a0ab4db77f821117227aaac153023908c4c7");
            var r = await test.GetFunction("registerDockingStation").SendTransactionAsync("0xb8C8316D7a3B401a835461C0AaF926C0caF5eF89", new object[] { "spha","spha","spha"});
            //RhodeITService rhodeIT = new RhodeITService(web3, test.Address);
            //var t = await rhodeIT.DockingStationExistsRequestAndWaitForReceiptAsync("Joe Slovo Male Residence", "26.5098542", "-33.3172424");
            //Console.WriteLine(t);


          
        }
    }

}
