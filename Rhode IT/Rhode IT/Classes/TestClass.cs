﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;

namespace Rhode_IT
{
    public class TestClass
    {
        SfPopupLayout myPopUp;
        Label popupContent;
        DataTemplate templateView;

        public TestClass()
        {
            myPopUp = new SfPopupLayout();
        }
        public async System.Threading.Tasks.Task deployAsync(string sn, string pass)
        {
            var senderAddress = "0xCbE8dc6Ce1014814F3f602e2cDE4035E0B6bd37D";
            var password = "password";
            var abi = @"[{'constant':false,'inputs':[{'name':'studentNo','type':'string'},{'name':'pass','type':'string'}],'name':'addUser','outputs':[{'name':'message','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'studentNo','type':'string'}],'name':'getUser','outputs':[{'name':'exists','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'}]";
            var byteCode = @"608060405234801561001057600080fd5b506106e3806100206000396000f30060806040526004361061004c576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff168063079eaf341461005157806331feb67114610118575b600080fd5b34801561005d57600080fd5b506100fe600480360381019080803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509192919290505050610199565b604051808215151515815260200191505060405180910390f35b34801561012457600080fd5b5061017f600480360381019080803590602001908201803590602001908080601f016020809104026020016040519081016040528093929190818152602001838380828437820191505050505050919291929050505061044a565b604051808215151515815260200191505060405180910390f35b60008073ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614151515610265576040517f08c379a000000000000000000000000000000000000000000000000000000000815260040180806020018281038252602a8152602001807f496e76616c69642073656e646572206164647265737320696e2061646455736581526020017f722066756e6374696f6e0000000000000000000000000000000000000000000081525060400191505060405180910390fd5b6000836040518082805190602001908083835b60208310151561029d5780518252602082019150602081019050602083039250610278565b6001836020036101000a038019825116818451168082178552505050505050905001915050908152602001604051809103902060020160009054906101000a900460ff16151515610356576040517f08c379a000000000000000000000000000000000000000000000000000000000815260040180806020018281038252601a8152602001807f53747564656e7420616c7265616479207265676973746572656400000000000081525060200191505060405180910390fd5b606060405190810160405280848152602001838152602001600115158152506000846040518082805190602001908083835b6020831015156103ad5780518252602082019150602081019050602083039250610388565b6001836020036101000a038019825116818451168082178552505050505050905001915050908152602001604051809103902060008201518160000190805190602001906103fc929190610612565b506020820151816001019080519060200190610419929190610612565b5060408201518160020160006101000a81548160ff0219169083151502179055509050506001905080905092915050565b60008073ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614151515610516576040517f08c379a000000000000000000000000000000000000000000000000000000000815260040180806020018281038252602a8152602001807f496e76616c69642073656e646572206164647265737320696e2066756e63746981526020017f6f6e20676574557365720000000000000000000000000000000000000000000081525060400191505060405180910390fd5b6000826040518082805190602001908083835b60208310151561054e5780518252602082019150602081019050602083039250610529565b6001836020036101000a038019825116818451168082178552505050505050905001915050908152602001604051809103902060020160009054906101000a900460ff161515610606576040517f08c379a00000000000000000000000000000000000000000000000000000000081526004018080602001828103825260168152602001807f53747564656e74206e6f7420726567697374657265640000000000000000000081525060200191505060405180910390fd5b60019050809050919050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061065357805160ff1916838001178555610681565b82800160010185558215610681579182015b82811115610680578251825591602001919060010190610665565b5b50905061068e9190610692565b5090565b6106b491905b808211156106b0576000816000905550600101610698565b5090565b905600a165627a7a72305820f32ba1133cb0841c434d6503e05fbd9922f5937a4421cab294ce19466f2a3e2c0029";
            var web3 = new Web3("http://146.231.123.144:7545/");
            web3.TransactionManager.DefaultGas = new System.Numerics.BigInteger(10000000);
            web3.TransactionManager.DefaultGasPrice = new System.Numerics.BigInteger(20000000000);
            var unlockAccountResult =await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120);
            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(byteCode, senderAddress);
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            while (receipt == null)
            {
                Thread.Sleep(5000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }
            var contractAddress = receipt.ContractAddress;
            var contract = web3.Eth.GetContract(abi, contractAddress);
            var addUser = contract.GetFunction("addUser");
            var getUser = contract.GetFunction("getUser");
            var gasLimit = new HexBigInteger(0x7A1200);
            var gasPrice = new HexBigInteger(0x4A817C800);
            var value = new HexBigInteger(0xB1A2BC2EC50000);
            var send = await addUser.SendTransactionAndWaitForReceiptAsync(senderAddress,null,new object[] { sn, pass});
            var result = await getUser.CallAsync<bool>(sn);
            //myPopUp = new SfPopupLayout();
            //Label label = new Label();
            //label.Text = "Login Results";
            //templateView = new DataTemplate(() => {
            //    popupContent = label;
            //    popupContent.Text = "Results to contract call: " + result.ToString();
            //    popupContent.BackgroundColor = Color.LightSkyBlue;
            //    popupContent.HorizontalTextAlignment = TextAlignment.Center;
            //    return popupContent;
            //});
            //// Adding ContentTemplate of the SfPopupLayout
            //myPopUp.PopupView.ContentTemplate = templateView;
            //myPopUp.PopupView.AnimationMode = AnimationMode.Fade;
            //myPopUp.Closed += PopupLayout_Closed;
            //myPopUp.Show();
        }
        private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {
            myPopUp.Show();
        }
        private void PopupLayout_Closed(object sender, EventArgs e)
        {

        }
    }
}