﻿//-----------------------------------------
//RhodeITDefinition.cs
//-----------------------------------------
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Collections.Generic;
using System.Numerics;

namespace RhodeIT.Services.RhodeIT.ContractDefinition
{


    public partial class RhodeITDeployment : RhodeITDeploymentBase
    {
        public RhodeITDeployment() : base(BYTECODE) { }
        public RhodeITDeployment(string byteCode) : base(byteCode) { }
    }

    public class RhodeITDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50600436106100ea5760003560e01c80637a64bfff1161008c578063d53ef06d11610066578063d53ef06d146101d6578063e0579807146101e9578063eae6d4da146101fc578063ec99d82314610204576100ea565b80637a64bfff1461019b5780639ded3f92146101ae578063ab84f853146101c3576100ea565b80633843e770116100c85780633843e7701461013e578063515e523814610160578063647f2753146101735780636e787ca514610188576100ea565b806313248d51146100ef57806318b8275a146101185780632d6b40051461012b575b600080fd5b6101026100fd366004611c79565b610219565b60405161010f9190612372565b60405180910390f35b610102610126366004611c3c565b6105e4565b610102610139366004611c79565b6106cf565b61015161014c366004611c3c565b610a27565b60405161010f93929190612380565b61010261016e366004611c3c565b610c9b565b61017b610d2f565b60405161010f9190612350565b610102610196366004611c3c565b610dbd565b6101516101a9366004611c3c565b610e34565b6101b661105a565b60405161010f91906124f2565b6101026101d1366004611ce2565b6110c2565b6101026101e4366004611ce2565b6112e1565b6101026101f7366004611d76565b611773565b61010261184d565b61020c611866565b60405161010f9190612361565b6000336102415760405162461bcd60e51b8152600401610238906124b2565b60405180910390fd5b3360009081526020819052604090206003015460ff166102735760405162461bcd60e51b815260040161023890612402565b6002826040516102839190612344565b9081526040519081900360200190206008015460ff166102b55760405162461bcd60e51b8152600401610238906123c2565b6002826040516102c59190612344565b9081526020016040518091039020600701836040516102e49190612344565b9081526040519081900360200190206003015460ff6101009091041661031c5760405162461bcd60e51b815260040161023890612452565b60028260405161032c9190612344565b90815260200160405180910390206007018360405161034b9190612344565b9081526040519081900360200190206003015460ff1661037d5760405162461bcd60e51b815260040161023890612442565b6007543360009081526020819052604090206002015410156103b15760405162461bcd60e51b815260040161023890612412565b600754336000908152602081905260409020600201546103d69163ffffffff61195b16565b5060006002836040516103e99190612344565b9081526020016040518091039020600701846040516104089190612344565b90815260405190819003602001812060030180549215156101000261ff00199093169290921790915560029061043f908490612344565b90815260200160405180910390206006016005846040516104609190612344565b9081526020016040518091039020600501548154811061047c57fe5b90600052602060002001600061049291906119af565b6002826040516104a29190612344565b9081526020016040518091039020600701836040516104c19190612344565b90815260405190819003602001902060006104dc82826119af565b6104ea6001830160006119af565b60006002830181905560038301805461ffff1916905561050e9060048401906119f6565b600582016000905560068201600061052691906119af565b50506002826040516105389190612344565b90815260405190819003602001902060060180549061055b906000198301611a14565b5060058360405161056c9190612344565b90815260405190819003602090810182206004018054600181018255600091825291812090910180546001600160a01b03191633179055906005906105b2908690612344565b90815260405190819003602001902060030180549115156101000261ff00199092169190911790555060015b92915050565b6000336106035760405162461bcd60e51b8152600401610238906124c2565b3360009081526020819052604090206003015460ff16156106365760405162461bcd60e51b8152600401610238906123d2565b33600090815260208181526040909120835161065a92600190920191850190611a3d565b505033600081815260208190526040812060038101805460ff1916600190811790915581546001600160a01b0319908116851790925580548082018255928190527fb10e2d527612073b26eecdfd717e6a320cf44b4afac2b0732d9fcbe2b7fa0cf69092018054909116909217909155919050565b6000336106ee5760405162461bcd60e51b8152600401610238906124b2565b3360009081526020819052604090206003015460ff166107205760405162461bcd60e51b8152600401610238906124d2565b6005836040516107309190612344565b9081526040519081900360200190206003015460ff166107625760405162461bcd60e51b815260040161023890612442565b6005836040516107729190612344565b9081526040519081900360200190206003015460ff61010090910416156107ab5760405162461bcd60e51b815260040161023890612422565b6002826040516107bb9190612344565b9081526040519081900360200190206008015460ff166107ed5760405162461bcd60e51b8152600401610238906123c2565b6005836040516107fd9190612344565b9081526040519081900360209081018220600401805460018101825560009182529190200180546001600160a01b03191633179055600290610840908490612344565b908152604051602091819003820190206006018054600181018083556000928352918390208651929361087a939190920191870190611a3d565b5050600160058460405161088e9190612344565b908152602001604051809103902060030160016101000a81548160ff021916908315150217905550816005846040516108c79190612344565b908152602001604051809103902060010190805190602001906108eb929190611a3d565b506005836040516108fc9190612344565b908152602001604051809103902060028360405161091a9190612344565b9081526020016040518091039020600701846040516109399190612344565b908152604051908190036020019020815461096a908290849060026000196101006001841615020190911604611abb565b5060018201816001019080546001816001161561010002031660029004610992929190611abb565b506002828101549082015560038083018054918301805460ff938416151560ff1990911617808255915461010090819004909316151590920261ff0019909116179055600480830180546109e99284019190611b30565b506005820154816005015560068201816006019080546001816001161561010002031660029004610a1b929190611abb565b50600195945050505050565b6060808033610a485760405162461bcd60e51b8152600401610238906124b2565b600584604051610a589190612344565b9081526040519081900360200190206003015460ff16610a8a5760405162461bcd60e51b815260040161023890612482565b600584604051610a9a9190612344565b90815260405190819003602001812090600590610ab8908790612344565b9081526020016040518091039020600101600586604051610ad99190612344565b9081526040805160209281900383018120855460026001821615610100026000190190911604601f81018590048502830185019093528282526006019290918591830182828015610b6b5780601f10610b4057610100808354040283529160200191610b6b565b820191906000526020600020905b815481529060010190602001808311610b4e57829003601f168201915b5050855460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815295985087945092508401905082828015610bf95780601f10610bce57610100808354040283529160200191610bf9565b820191906000526020600020905b815481529060010190602001808311610bdc57829003601f168201915b5050845460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815295975086945092508401905082828015610c875780601f10610c5c57610100808354040283529160200191610c87565b820191906000526020600020905b815481529060010190602001808311610c6a57829003601f168201915b505050505090509250925092509193909250565b600033610cba5760405162461bcd60e51b8152600401610238906124b2565b600582604051610cca9190612344565b9081526040519081900360200190206003015460ff16610cfc5760405162461bcd60e51b815260040161023890612442565b600582604051610d0c9190612344565b9081526040519081900360200190206003015460ff610100909104169050919050565b6004546060906001600160a01b03163314610d5c5760405162461bcd60e51b8152600401610238906124a2565b6001805480602002602001604051908101604052809291908181526020018280548015610db257602002820191906000526020600020905b81546001600160a01b03168152600190910190602001808311610d94575b505050505090505b90565b6004546000906001600160a01b03163314610dea5760405162461bcd60e51b8152600401610238906124a2565b33610e075760405162461bcd60e51b8152600401610238906124b2565b600282604051610e179190612344565b9081526040519081900360200190206008015460ff169050919050565b6060808033610e555760405162461bcd60e51b8152600401610238906124b2565b600284604051610e659190612344565b90815260408051602092819003830181206001908101805460029281161561010002600019011691909104601f81018590048502830185019093528282529092909190830182828015610ef95780601f10610ece57610100808354040283529160200191610ef9565b820191906000526020600020905b815481529060010190602001808311610edc57829003601f168201915b50505050509250600284604051610f109190612344565b9081526040805160209281900383018120600301805460026001821615610100026000190190911604601f81018590048502830185019093528282529092909190830182828015610fa25780601f10610f7757610100808354040283529160200191610fa2565b820191906000526020600020905b815481529060010190602001808311610f8557829003601f168201915b50505050509150600284604051610fb99190612344565b90815260408051918290036020908101832060029081018054601f60001961010060018416150201909116929092049182018390048302850183019093528084529083018282801561104c5780601f106110215761010080835404028352916020019161104c565b820191906000526020600020905b81548152906001019060200180831161102f57829003601f168201915b505050505090509193909250565b6000336110795760405162461bcd60e51b815260040161023890612472565b3360009081526020819052604090206003015460ff166110ab5760405162461bcd60e51b8152600401610238906123e2565b503360009081526020819052604090206002015490565b6004546000906001600160a01b031633146110ef5760405162461bcd60e51b8152600401610238906124a2565b3361110c5760405162461bcd60e51b8152600401610238906124b2565b60028460405161111c9190612344565b9081526040519081900360200190206008015460ff161561114f5760405162461bcd60e51b815260040161023890612462565b836002856040516111609190612344565b90815260200160405180910390206000019080519060200190611184929190611a3d565b50836002856040516111969190612344565b908152602001604051809103902060010160000190805190602001906111bd929190611a3d565b50816002856040516111cf9190612344565b908152602001604051809103902060010160010190805190602001906111f6929190611a3d565b50826002856040516112089190612344565b9081526020016040518091039020600101600201908051906020019061122f929190611a3d565b5060006002856040516112429190612344565b90815260405160209181900382019020600501919091556003805460018101808355600092909252865191926112a0927fc2575a0e9e593c00f959f8c92f12db2869c3395a3b0502d05e2516446f71f85b9092019190880190611a3d565b505060016002856040516112b49190612344565b908152604051908190036020019020600801805491151560ff199092169190911790555060019392505050565b6004546000906001600160a01b0316331461130e5760405162461bcd60e51b8152600401610238906124a2565b60058460405161131e9190612344565b9081526040519081900360200190206003015460ff16156113515760405162461bcd60e51b815260040161023890612492565b6002826040516113619190612344565b9081526040519081900360200190206008015460ff166113935760405162461bcd60e51b8152600401610238906123c2565b6002826040516113a39190612344565b9081526020016040518091039020600701846040516113c29190612344565b9081526040519081900360200190206003015460ff16156113f55760405162461bcd60e51b815260040161023890612422565b836005856040516114069190612344565b9081526020016040518091039020600001908051906020019061142a929190611a3d565b508160058560405161143c9190612344565b90815260200160405180910390206001019080519060200190611460929190611a3d565b5060006005856040516114739190612344565b908152602001604051809103902060020181905550600160058560405161149a9190612344565b908152604051908190036020018120600301805492151560ff19909316929092179091556001906005906114cf908790612344565b908152602001604051809103902060030160016101000a81548160ff021916908315150217905550826005856040516115089190612344565b9081526020016040518091039020600601908051906020019061152c929190611a3d565b5060028260405161153d9190612344565b90815260200160405180910390206005015460058560405161155f9190612344565b90815260405160209181900382019020600501919091556006805460018101808355600092909252865191926115bd927ff652222313e28459528d920b65115c16c04f3efc82aaedc97be59f3f377c0d3f9092019190880190611a3d565b50506002826040516115cf9190612344565b9081526040516020918190038201902060060180546001810180835560009283529183902087519293611609939190920191880190611a3d565b505060058460405161161b9190612344565b90815260200160405180910390206002836040516116399190612344565b9081526020016040518091039020600701856040516116589190612344565b9081526040519081900360200190208154611689908290849060026000196101006001841615020190911604611abb565b50600182018160010190805460018160011615610100020316600290046116b1929190611abb565b506002828101549082015560038083018054918301805460ff938416151560ff1990911617808255915461010090819004909316151590920261ff0019909116179055600480830180546117089284019190611b30565b50600582015481600501556006820181600601908054600181600116156101000203166002900461173a929190611abb565b5090505060028260405161174e9190612344565b9081526040519081900360200190206005018054600190810190915590509392505050565b6004546000906001600160a01b031633146117a05760405162461bcd60e51b8152600401610238906124a2565b336117bd5760405162461bcd60e51b815260040161023890612472565b3360009081526020819052604090206003015460ff166117ef5760405162461bcd60e51b8152600401610238906124d2565b6000821161180f5760405162461bcd60e51b8152600401610238906124e2565b33600090815260208190526040902060020154611832908363ffffffff61198316565b33600090815260208190526040902060020155506001919050565b3360009081526020819052604090206003015460ff1690565b6060336118855760405162461bcd60e51b8152600401610238906124b2565b6003805480602002602001604051908101604052809291908181526020016000905b828210156119525760008481526020908190208301805460408051601f600260001961010060018716150201909416939093049283018590048502810185019091528181529283018282801561193e5780601f106119135761010080835404028352916020019161193e565b820191906000526020600020905b81548152906001019060200180831161192157829003601f168201915b5050505050815260200190600101906118a7565b50505050905090565b60008282111561197d5760405162461bcd60e51b815260040161023890612432565b50900390565b6000828201838110156119a85760405162461bcd60e51b8152600401610238906123f2565b9392505050565b50805460018160011615610100020316600290046000825580601f106119d557506119f3565b601f0160209004906000526020600020908101906119f39190611b7c565b50565b50805460008255906000526020600020908101906119f39190611b7c565b815481835581811115611a3857600083815260209020611a38918101908301611b96565b505050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10611a7e57805160ff1916838001178555611aab565b82800160010185558215611aab579182015b82811115611aab578251825591602001919060010190611a90565b50611ab7929150611b7c565b5090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10611af45780548555611aab565b82800160010185558215611aab57600052602060002091601f016020900482015b82811115611aab578254825591600101919060010190611b15565b828054828255906000526020600020908101928215611b705760005260206000209182015b82811115611b70578254825591600101919060010190611b55565b50611ab7929150611bb9565b610dba91905b80821115611ab75760008155600101611b82565b610dba91905b80821115611ab7576000611bb082826119af565b50600101611b9c565b610dba91905b80821115611ab75780546001600160a01b0319168155600101611bbf565b600082601f830112611bee57600080fd5b8135611c01611bfc82612527565b612500565b91508082526020830160208301858383011115611c1d57600080fd5b611c28838284612583565b50505092915050565b80356105de816125c9565b600060208284031215611c4e57600080fd5b813567ffffffffffffffff811115611c6557600080fd5b611c7184828501611bdd565b949350505050565b60008060408385031215611c8c57600080fd5b823567ffffffffffffffff811115611ca357600080fd5b611caf85828601611bdd565b925050602083013567ffffffffffffffff811115611ccc57600080fd5b611cd885828601611bdd565b9150509250929050565b600080600060608486031215611cf757600080fd5b833567ffffffffffffffff811115611d0e57600080fd5b611d1a86828701611bdd565b935050602084013567ffffffffffffffff811115611d3757600080fd5b611d4386828701611bdd565b925050604084013567ffffffffffffffff811115611d6057600080fd5b611d6c86828701611bdd565b9150509250925092565b600060208284031215611d8857600080fd5b6000611c718484611c31565b6000611da08383611db4565b505060200190565b60006119a88383611e93565b611dbd81612567565b82525050565b6000611dce82612555565b611dd88185612559565b9350611de38361254f565b8060005b83811015611e11578151611dfb8882611d94565b9750611e068361254f565b925050600101611de7565b509495945050505050565b6000611e2782612555565b611e318185612559565b935083602082028501611e438561254f565b8060005b85811015611e7d5784840389528151611e608582611da8565b9450611e6b8361254f565b60209a909a0199925050600101611e47565b5091979650505050505050565b611dbd81612572565b6000611e9e82612555565b611ea88185612559565b9350611eb881856020860161258f565b611ec1816125bf565b9093019392505050565b6000611ed682612555565b611ee08185612562565b9350611ef081856020860161258f565b9290920192915050565b6000611f07601c83612559565b7f446f636b696e672073746174696f6e20646f65736e7420657869737400000000815260200192915050565b6000611f40601783612559565b7f7573657220616c72656164792072656769737465726564000000000000000000815260200192915050565b6000611f79601383612559565b72155cd95c881b9bdd081c9959da5cdd195c9959606a1b815260200192915050565b6000611fa8601b83612559565b7f536166654d6174683a206164646974696f6e206f766572666c6f770000000000815260200192915050565b6000611fe1601483612559565b731d5cd95c881b9bdd081c995a59da5cdd195c995960621b815260200192915050565b6000612011602583612559565b7f496e73756666696369656e7420636f737420746f20626f72726f7720612062698152646379636c6560d81b602082015260400192915050565b6000612058601683612559565b75109a58de58db1948185b1c9958591e48191bd8dad95960521b815260200192915050565b600061208a601e83612559565b7f536166654d6174683a207375627472616374696f6e206f766572666c6f770000815260200192915050565b60006120c3601683612559565b75109a58de58db19481b9bdd081c9959da5cdd195c995960521b815260200192915050565b60006120f5601283612559565b71109a58de58db19481b9bdd08191bd8dad95960721b815260200192915050565b6000612123601d83612559565b7f446f636b696e672053746174696f6e20616c7265616479206578697473000000815260200192915050565b600061215c602f83612559565b7f496e76616c69642073656e646572206164647265737320696e2075706461746581526e21b932b234ba10333ab731ba34b7b760891b602082015260400192915050565b60006121ad601483612559565b73109a58de58db1948191bd95cdb9d08195e1a5cdd60621b815260200192915050565b60006121dd601583612559565b74109a58de58db1948185b1c9958591e481859191959605a1b815260200192915050565b600061220e602883612559565b7f4f6e6c792061646d696e20616c6c6f77656420746f2063616c6c207468697320815267333ab731ba34b7b760c11b602082015260400192915050565b6000612258601683612559565b75496e76616c69642073656e646572206164647265737360501b815260200192915050565b600061228a602a83612559565b7f496e76616c69642073656e646572206164647265737320696e206164645573658152693910333ab731ba34b7b760b11b602082015260400192915050565b60006122d6601383612559565b721d5cd95c881b9bdd081c9959da5cdd195c9959606a1b815260200192915050565b6000612305602183612559565b7f6e657720637265646974206d7573742062652067726561746572207468616e208152600360fc1b602082015260400192915050565b611dbd81610dba565b60006119a88284611ecb565b602080825281016119a88184611dc3565b602080825281016119a88184611e1c565b602081016105de8284611e8a565b606080825281016123918186611e93565b905081810360208301526123a58185611e93565b905081810360408301526123b98184611e93565b95945050505050565b602080825281016105de81611efa565b602080825281016105de81611f33565b602080825281016105de81611f6c565b602080825281016105de81611f9b565b602080825281016105de81611fd4565b602080825281016105de81612004565b602080825281016105de8161204b565b602080825281016105de8161207d565b602080825281016105de816120b6565b602080825281016105de816120e8565b602080825281016105de81612116565b602080825281016105de8161214f565b602080825281016105de816121a0565b602080825281016105de816121d0565b602080825281016105de81612201565b602080825281016105de8161224b565b602080825281016105de8161227d565b602080825281016105de816122c9565b602080825281016105de816122f8565b602081016105de828461233b565b60405181810167ffffffffffffffff8111828210171561251f57600080fd5b604052919050565b600067ffffffffffffffff82111561253e57600080fd5b506020601f91909101601f19160190565b60200190565b5190565b90815260200190565b919050565b60006105de82612577565b151590565b6001600160a01b031690565b82818337506000910152565b60005b838110156125aa578181015183820152602001612592565b838111156125b9576000848401525b50505050565b601f01601f191690565b6125d281610dba565b81146119f357600080fdfea365627a7a723058202389956d605cf8b732e1bc61f62580971c3b63b09abc3bdb4bf139142b05fa896c6578706572696d656e74616cf564736f6c63430005090040";
        public RhodeITDeploymentBase() : base(BYTECODE) { }
        public RhodeITDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class RentBicycleFunction : RentBicycleFunctionBase { }

    [Function("rentBicycle", "bool")]
    public class RentBicycleFunctionBase : FunctionMessage
    {
        [Parameter("string", "bicycleId", 1)]
        public virtual string BicycleId { get; set; }
        [Parameter("string", "dockingStation", 2)]
        public virtual string DockingStation { get; set; }
    }

    public partial class AddUserFunction : AddUserFunctionBase { }

    [Function("addUser", "bool")]
    public class AddUserFunctionBase : FunctionMessage
    {
        [Parameter("string", "studentno_staff_no", 1)]
        public virtual string Studentno_staff_no { get; set; }
    }

    public partial class DockBicycleFunction : DockBicycleFunctionBase { }

    [Function("dockBicycle", "bool")]
    public class DockBicycleFunctionBase : FunctionMessage
    {
        [Parameter("string", "bicycleId", 1)]
        public virtual string BicycleId { get; set; }
        [Parameter("string", "dockingStation", 2)]
        public virtual string DockingStation { get; set; }
    }

    public partial class GetBicycleFunction : GetBicycleFunctionBase { }

    [Function("getBicycle", typeof(GetBicycleOutputDTO))]
    public class GetBicycleFunctionBase : FunctionMessage
    {
        [Parameter("string", "bicycleId", 1)]
        public virtual string BicycleId { get; set; }
    }

    public partial class BicycleDockedFunction : BicycleDockedFunctionBase { }

    [Function("bicycleDocked", "bool")]
    public class BicycleDockedFunctionBase : FunctionMessage
    {
        [Parameter("string", "bicycleId", 1)]
        public virtual string BicycleId { get; set; }
    }

    public partial class GetAllRegisteredUserKeysFunction : GetAllRegisteredUserKeysFunctionBase { }

    [Function("getAllRegisteredUserKeys", "address[]")]
    public class GetAllRegisteredUserKeysFunctionBase : FunctionMessage
    {

    }

    public partial class DockingStationExistsFunction : DockingStationExistsFunctionBase { }

    [Function("dockingStationExists", "bool")]
    public class DockingStationExistsFunctionBase : FunctionMessage
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
    }

    public partial class GetDockingStationFunction : GetDockingStationFunctionBase { }

    [Function("getDockingStation", typeof(GetDockingStationOutputDTO))]
    public class GetDockingStationFunctionBase : FunctionMessage
    {
        [Parameter("string", "stationName", 1)]
        public virtual string StationName { get; set; }
    }

    public partial class GetUsercreditFunction : GetUsercreditFunctionBase { }

    [Function("getUsercredit", "uint256")]
    public class GetUsercreditFunctionBase : FunctionMessage
    {

    }

    public partial class RegisterDockingStationFunction : RegisterDockingStationFunctionBase { }

    [Function("registerDockingStation", "bool")]
    public class RegisterDockingStationFunctionBase : FunctionMessage
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("string", "latitude", 2)]
        public virtual string Latitude { get; set; }
        [Parameter("string", "longitude", 3)]
        public virtual string Longitude { get; set; }
    }

    public partial class RegisterNewBicycleFunction : RegisterNewBicycleFunctionBase { }

    [Function("registerNewBicycle", "bool")]
    public class RegisterNewBicycleFunctionBase : FunctionMessage
    {
        [Parameter("string", "bicycleId", 1)]
        public virtual string BicycleId { get; set; }
        [Parameter("string", "features", 2)]
        public virtual string Features { get; set; }
        [Parameter("string", "dockingStation", 3)]
        public virtual string DockingStation { get; set; }
    }

    public partial class UpdateCreditFunction : UpdateCreditFunctionBase { }

    [Function("updateCredit", "bool")]
    public class UpdateCreditFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "credit", 1)]
        public virtual BigInteger Credit { get; set; }
    }

    public partial class UserExistsFunction : UserExistsFunctionBase { }

    [Function("userExists", "bool")]
    public class UserExistsFunctionBase : FunctionMessage
    {

    }

    public partial class GetRegisteredDockingStationKeysFunction : GetRegisteredDockingStationKeysFunctionBase { }

    [Function("getRegisteredDockingStationKeys", "string[]")]
    public class GetRegisteredDockingStationKeysFunctionBase : FunctionMessage
    {

    }

    public partial class AddUserLoggerEventDTO : AddUserLoggerEventDTOBase { }

    [Event("addUserLogger")]
    public class AddUserLoggerEventDTOBase : IEventDTO
    {
        [Parameter("bool", "results", 1, true)]
        public virtual bool Results { get; set; }
    }

    public partial class UserExistsLoggerEventDTO : UserExistsLoggerEventDTOBase { }

    [Event("userExistsLogger")]
    public class UserExistsLoggerEventDTOBase : IEventDTO
    {
        [Parameter("string", "tHash", 1, true)]
        public virtual string THash { get; set; }
        [Parameter("bool", "results", 2, true)]
        public virtual bool Results { get; set; }
    }

    public partial class AddDockingStationLoggerEventDTO : AddDockingStationLoggerEventDTOBase { }

    [Event("addDockingStationLogger")]
    public class AddDockingStationLoggerEventDTOBase : IEventDTO
    {
        [Parameter("string", "tHash", 1, true)]
        public virtual string THash { get; set; }
        [Parameter("bool", "results", 2, true)]
        public virtual bool Results { get; set; }
    }

    public partial class DockingStationExistsLoggerEventDTO : DockingStationExistsLoggerEventDTOBase { }

    [Event("dockingStationExistsLogger")]
    public class DockingStationExistsLoggerEventDTOBase : IEventDTO
    {
        [Parameter("string", "tHash", 1, true)]
        public virtual string THash { get; set; }
        [Parameter("bool", "results", 2, true)]
        public virtual bool Results { get; set; }
    }

    public partial class FoundDockingStationEventEventDTO : FoundDockingStationEventEventDTOBase { }

    [Event("foundDockingStationEvent")]
    public class FoundDockingStationEventEventDTOBase : IEventDTO
    {
        [Parameter("string", "name", 1, true)]
        public virtual string Name { get; set; }
        [Parameter("string", "latitude", 2, true)]
        public virtual string Latitude { get; set; }
        [Parameter("string", "longitude", 3, true)]
        public virtual string Longitude { get; set; }
    }







    public partial class GetBicycleOutputDTO : GetBicycleOutputDTOBase { }

    [FunctionOutput]
    public class GetBicycleOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("string", "", 2)]
        public virtual string ReturnValue2 { get; set; }
        [Parameter("string", "", 3)]
        public virtual string ReturnValue3 { get; set; }
    }

    public partial class BicycleDockedOutputDTO : BicycleDockedOutputDTOBase { }

    [FunctionOutput]
    public class BicycleDockedOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetAllRegisteredUserKeysOutputDTO : GetAllRegisteredUserKeysOutputDTOBase { }

    [FunctionOutput]
    public class GetAllRegisteredUserKeysOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class DockingStationExistsOutputDTO : DockingStationExistsOutputDTOBase { }

    [FunctionOutput]
    public class DockingStationExistsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetDockingStationOutputDTO : GetDockingStationOutputDTOBase { }

    [FunctionOutput]
    public class GetDockingStationOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("string", "latitude", 2)]
        public virtual string Latitude { get; set; }
        [Parameter("string", "longitude", 3)]
        public virtual string Longitude { get; set; }
    }

    public partial class GetUsercreditOutputDTO : GetUsercreditOutputDTOBase { }

    [FunctionOutput]
    public class GetUsercreditOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class UserExistsOutputDTO : UserExistsOutputDTOBase { }

    [FunctionOutput]
    public class UserExistsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetRegisteredDockingStationKeysOutputDTO : GetRegisteredDockingStationKeysOutputDTOBase { }

    [FunctionOutput]
    public class GetRegisteredDockingStationKeysOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

}
