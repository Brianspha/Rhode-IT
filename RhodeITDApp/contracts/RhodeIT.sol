
/**
 *@Contract to help in the running of the Bicycle Sharing app
 */
contract RhodeIT  {

/**
 * @dev represents a student 
 * @param studentNo represents a student no used as ID on the platform
 * @param password student choosen password
 * @param usedCount repersents the total number of bicycles used by student thus far
 * @param Active used to check if a student exists on the system or not
 */
struct Student{
    string studentNo;
    string password;
    uint256 usedCount;
    bool Active; 
    mapping(uint256 => Bicycle) UsedBicycles;
}
event addUserLogger(string  indexed userID,bool indexed results );
event userExistsLogger(string   indexed userID,bool  indexed results);
event addDockingStationLogger(string  indexed name,string  indexed Latitude,string  indexed Longitude,bool results );
event dockingStationExistsLogger(string   indexed name,bool indexed results);


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
    mapping(string=> Student) UserHistory;
}
/**
 * @dev represents a venue to be used as a DockingStation
 * @param Name of the venue on Campus
 * @notice since solidity doesnt allow for floating points the the Latitude and Longitude have been broken into 2 parts i.e 26.9999 will become 26 and the 9999
 * @param Latitude position on map   
 * @param LatitudeP2 second part of Latitude after the .
 * @param Longitude position on map
 * @param LongitudeP2 second part after the .
 * @param indicates if instance exists or not
 */ 
struct VenueLocation{
    string  Name;
    string  Latitude;
    string Longitude;
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
mapping (string  => Student) Students;
/**
 *@dev stores all registed docking DockingStations on the platform
 */ 
mapping (string  => DockingStation) DockingStations;

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
* @param pass represents the students password
*/
function addUser(string memory  studentNo, string memory   pass) public returns (bool ){
    require(msg.sender != address(0),"Invalid sender address in addUser function");
    Students[studentNo] = Student(studentNo,pass,0,true);
    emit addUserLogger(studentNo,true);
    return true;
}
/**
 * @dev responsible for checking if a particular user exists
 * @param studentNo studentNo to verify 
 * @return if exist or not
 * 
 */
function userExists(string memory  studentNo) public  returns (bool)
{
    require(msg.sender != address(0),"Invalid sender address in function getUser");
    emit userExistsLogger(studentNo,Students[studentNo].Active);
    return Students[studentNo].Active;
}

/**
 * @dev responsible for adding a new docking station
 */ 
function registerDockingStation(string memory  name,string memory  latitude, string memory  longitude) public returns(bool){
 /**
  * @dev ensure that the sender address is valid
 */
 require(msg.sender != address(0),"Invalid sender address");
 VenueLocation memory temp =VenueLocation(name,latitude,longitude,true);
 DockingStations[name]=DockingStation(temp,0,true);
 countDockingStations+=1;
 emit addDockingStationLogger(name,latitude,longitude,true);
 return true;
}
/**
 *@dev responsible for checking if a docking is registered
 * @param name the DockingStation name to search for
 * 
 */
function DockingStationExists(string memory  name) public returns (bool){
require(msg.sender != address(0),"Invalid sender address");
emit dockingStationExistsLogger(name,DockingStations[name].Active);
return DockingStations[name].Active;
}

}