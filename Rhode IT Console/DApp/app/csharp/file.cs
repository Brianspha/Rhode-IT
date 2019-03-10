using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;

namespace Rhode_IT {
    public class TestClass {
        SfPopupLayout myPopUp;
        Label popupContent;
        DataTemplate templateView;

        public TestClass () {
            myPopUp = new SfPopupLayout ();
        }
        public async System.Threading.Tasks.Task deployAsync (string sn, string pass) {
            var senderAddress = "0xCbE8dc6Ce1014814F3f602e2cDE4035E0B6bd37D";
            var password = "password";
            var abi = @"[{'constant':false,'inputs':[{'name':'studentNo','type':'string'},{'name':'pass','type':'string'}],'name':'addUser','outputs':[{'name':'message','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
            var byteCode =
                @"608060405234801561001057600080fd5b5061048f806100206000396000f300608060405260043610610041576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff168063079eaf3414610046575b600080fd5b34801561005257600080fd5b506100f3600480360381019080803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f016020809104026020016040519081016040528093929190818152602001838380828437820191505050505050919291929050505061010d565b604051808215151515815260200191505060405180910390f35b60008073ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141515156101d9576040517f08c379a000000000000000000000000000000000000000000000000000000000815260040180806020018281038252602a8152602001807f496e76616c69642073656e646572206164647265737320696e2061646455736581526020017f722066756e6374696f6e0000000000000000000000000000000000000000000081525060400191505060405180910390fd5b6000836040518082805190602001908083835b60208310151561021157805182526020820191506020810190506020830392506101ec565b6001836020036101000a038019825116818451168082178552505050505050905001915050908152602001604051809103902060020160009054906101000a900460ff161515156102ca576040517f08c379a000000000000000000000000000000000000000000000000000000000815260040180806020018281038252601a8152602001807f53747564656e7420616c7265616479207265676973746572656400000000000081525060200191505060405180910390fd5b606060405190810160405280848152602001838152602001600115158152506000846040518082805190602001908083835b60208310151561032157805182526020820191506020810190506020830392506102fc565b6001836020036101000a038019825116818451168082178552505050505050905001915050908152602001604051809103902060008201518160000190805190602001906103709291906103be565b50602082015181600101908051906020019061038d9291906103be565b5060408201518160020160006101000a81548160ff0219169083151502179055509050506001905080905092915050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106103ff57805160ff191683800117855561042d565b8280016001018555821561042d579182015b8281111561042c578251825591602001919060010190610411565b5b50905061043a919061043e565b5090565b61046091905b8082111561045c576000816000905550600101610444565b5090565b905600a165627a7a72305820b7553f4d92985869372d931eb5956986b9ce67aa64ee912c7e2a78dc9eeed6190029";

            var multiplier = 7;

            var web3 = new Web3 ("http://146.231.123.144:7545/");
            web3.TransactionManager.DefaultGas = new System.Numerics.BigInteger (10000000);
            web3.TransactionManager.DefaultGasPrice = new System.Numerics.BigInteger (20000000000);
            var unlockAccountResult =
                await web3.Personal.UnlockAccount.SendRequestAsync (senderAddress, password, 120);

            var transactionHash =
                await web3.Eth.DeployContract.SendRequestAsync (abi, byteCode, senderAddress, multiplier);

            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync (transactionHash);

            while (receipt == null) {
                Thread.Sleep (5000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync (transactionHash);
            }
            var contractAddress = receipt.ContractAddress;
            var contract = web3.Eth.GetContract (abi, contractAddress);
            var addUser = contract.GetFunction ("addUser");
            var gasLimit = new HexBigInteger (0x7A1200);
            var gasPrice = new HexBigInteger (0x4A817C800);
            var value = new HexBigInteger (0xB1A2BC2EC50000);
            var result = await addUser.CallAsync<string> (sn, pass);
            myPopUp = new SfPopupLayout ();
            Label label = new Label ();
            label.Text = "Login Results";
            templateView = new DataTemplate (() => {
                popupContent = label;
                popupContent.Text = "Results to contract call: " + result.ToString ();
                popupContent.BackgroundColor = Color.LightSkyBlue;
                popupContent.HorizontalTextAlignment = TextAlignment.Center;
                return popupContent;
            });

            // Adding ContentTemplate of the SfPopupLayout
            myPopUp.PopupView.ContentTemplate = templateView;
            myPopUp.PopupView.AnimationMode = AnimationMode.Fade;
            myPopUp.Closed += PopupLayout_Closed;
            myPopUp.Show ();
        }

        private void ClickToShowPopup_Clicked (object sender, EventArgs e) {
            myPopUp.Show ();
        }
        private void PopupLayout_Closed (object sender, EventArgs e) {

        }
    }
}