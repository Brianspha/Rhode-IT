 pragma solidity  ^0.5.0;


/**
 *@Contract to help in the running of the Bicycle Sharing app
 */
contract RhodeIT  {


/**
 * @dev represents a student
 * @param stdNo represents a student registertation id 
 * @param studentNopassword represents a student no and password (hash) used as ID on the platform
 * @param Active used to check if a student exists on the system or not
 */
struct Student{
    bytes32 stdNo; 
    uint256 usedCount;
    bool Active; 
    mapping(uint256 => Bicycle) UsedBicycles;
}

event addUserLogger(bool indexed results );
event userExistsLogger(bytes32   indexed tHash,bool  indexed results);
event addDockingStationLogger(bytes32  indexed tHash,bool indexed results );
event dockingStationExistsLogger(bytes32   indexed tHash,bool indexed results);


/**
 * @dev represents a bicycle to be used on the platform
 * @param ID unique identification for the current instance of the Bicycle
 * @param userCount keeps track of the total no of students who hav used the bicycle used for iterating through @UserHistory
 * @param UserHistory used to keep track of all students who have used the Bicycle
 * @param indicates if instance exists or not
 */
struct Bicycle{
    uint256 ID;
    uint256 userCount;
    bool Active;
    mapping(bytes32=> Student) UserHistory;
}
/**
 * @dev represents a venue to be used as a DockingStation
 * @param NameLatLong represents the hash of the name Latitude and Longitude of a venue 
 * @param indicates if instance exists or not
 */ 
struct VenueLocation{
    bytes32 NameLatLong;
    bool Active;
}

/**
 * @dev represents a DockingStation on Campus
 * @param VenueLocation represents the DockingStation on Campus
 * @param AvailableBicycles stores AvailableBicycles on the DockingStation
 * @param Active indicates is instace exists or not
 */ 
struct DockingStation{
    VenueLocation DockingStationInformation;
    uint256 count;
    mapping(uint256=> Bicycle) AvailableBicycles;
    bool Active;
}
/**
 * @dev stores all registered students on platform
 */ 
mapping (bytes32  => Student) Students;
/**
 *@dev stores all registed docking DockingStations on the platform
 */ 
mapping (bytes32  => DockingStation) DockingStations;

/**
 * @dev keeps track of the total no of registered DockingStations
 */ 
uint256 countDockingStations=0;
/**
 * @dev keeps track of the total no of registered students
 */ 
uint256 countStudents;

/**
 *@dev represents the owner of the platform 
 */
address owner;



/**
 *@dev modifiers section start 
 */
 
 /**
  * @dev function ensures that only is allowed to call functions marked with onlyOwner
  * /
 modifier onlyOwner(){
     require(msg.sender ==owner);
     _;
 }
 
 /**
  *@dev modifiers section end 
  */

/**
 *@dev called when contract is created 
 */
constructor () public{
    /**
     *@dev ensure that address is valid 
     */
    require(msg.sender != address(0));
    owner=msg.sender;
}


/**
*@dev responsible for adding a new student on the  database
* @param studentNo represents the student identification no
* @notice empty variable checking is done outside of the blockchain 
*/
function addUser(string  memory  studentNo) public returns (bool ){
    require(msg.sender != address(0),"Invalid sender address in addUser function");
    bytes32 stdNo=keccak256(abi.encode(studentNo));
    Students[stdNo] = Student(stdNo,0,true);//@dev compute hash of studentNo+pass
    emit addUserLogger(true);
    return true;
}

/**
 * @dev responsible for checking if a particular user exists
 * @param studentNo studentNo to verify  * @notice empty variable checking is done outside of the blockchain 
 * @return if exist or not
 * 
 */
function userExists(string memory studentNo) public  returns (bool)
{
    require(msg.sender != address(0),"Invalid sender address in function getUser");
    bytes32 hash=keccak256(abi.encode(studentNo));
    emit userExistsLogger(hash,Students[hash].Active);
    return Students[hash].Active;
}

/**
 * @dev responsible for adding a new docking station
 *  * @notice empty variable checking is done outside of the blockchain 
 */ 
function registerDockingStation(string memory name,string memory  latitude, string memory  longitude) public returns(bool){
 /**
  * @dev ensure that the sender address is valid
 */
 require(msg.sender != address(0),"Invalid sender address");
  bytes32 hash=keccak256(abi.encode(name,latitude,longitude));
 VenueLocation memory temp =VenueLocation(hash,true);
 DockingStations[hash]=DockingStation(temp,0,true);
 countDockingStations+=1;
 emit addDockingStationLogger(hash,true);
 return true;
}
/**
 *@dev responsible for checking if a docking is registered
 * @param name the DockingStation name to search for
 * @notice empty variable checking is done outside of the blockchain 
 */
function DockingStationExists(string memory name,string memory  latitude, string memory  longitude) public returns (bool){
require(msg.sender != address(0),"Invalid sender address");
bytes32 hash=keccak256(abi.encode(name,latitude,longitude));
emit dockingStationExistsLogger(hash,DockingStations[hash].Active);
return DockingStations[hash].Active;
}



}