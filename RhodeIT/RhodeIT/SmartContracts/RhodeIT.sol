 pragma solidity >= 0.5 .0;


 /**
 *@Contract to help in the running of the Bicycle Sharing app
 @notice the contract is not optimised for gas because of the C# API not supporting certain web3 API calls
 */
 contract RhodeIT {


     /*====================*Struct section start ====================*/
     /**
      * @dev represents a student
      * @param studentNo represents a student registertation id
      * @param studentNopassword represents a student no and password (hash) used as ID on the platform
      * @param active used to check if a student exists on the system or not
      */
     struct Student {
         string studentNo;
         uint256 usedCount;
         bool active;
         mapping(uint256 => Bicycle) usedBicycles;
     }

     event addUserLogger(bool indexed results);
     event userExistsLogger(string indexed tHash, bool indexed results);
     event addDockingStationLogger(bytes32 indexed tHash, bool indexed results);
     event dockingStationExistsLogger(bytes32 indexed tHash, bool indexed results);


     /**
     * @dev represents a bicycle to be used on the platform
     * @param id unique identification for the current instance of the Bicycle
     * @param userCount keeps track of the total no of students who hav used the bicycle used for iterating through
     @UserHistory
     * @param userHistory used to keep track of all students who have used the Bicycle
     * @param indicates if instance exists or not
     */
     struct Bicycle {
         uint256 id;
         uint256 userCount;
         bool active;
         mapping(bytes32 => Student) userHistory;
     }
     /**
      * @dev represents a venue to be used as a DockingStation
      * @param name the name of the docking station on campus
        @param lat position on map
        @param long position on map
      * @param active indicates if instance exists or not
      */
     struct VenueLocation {
         string name;
         string longitude;
         string latitude;
         bool active;
     }

     /**
     * @dev represents a DockingStation on Campus
     @param id represents the index of the Dockingstation on the Global mapping of DockingStations
     * @param venueLocation represents the DockingStation on Campus
     * @param availableBicycles stores AvailableBicycles on the DockingStation
     * @param active indicates is instace exists or not
     *@param availableBicyclesKeys used to store all bicycle keys of available bicycles at the current docking station
     */
     struct DockingStation {
         bytes32 id;
         VenueLocation dockingStationInformation;
         uint256 count;
         uint256[] availableBicyclesKeys;
         mapping(uint256 => Bicycle) availableBicycles;
         bool active;
     }


     /*====================*Contract variable section start ====================*/
     /**
      * @dev stores all registered students on platform
      */
     mapping(string => Student) Students;
     /**
      *@dev stores all registed docking DockingStations on the platform
      */
     mapping(bytes32 => DockingStation) dockingStations;
     bytes32[] dockingStationKeys;
     /**
      * @dev keeps track of the total no of registered DockingStations
      */
     uint256 countDockingStations = 0;
     /**
      * @dev keeps track of the total no of registered students
      */
     uint256 countStudents;

     /**
      *@dev represents the owner of the platform
      */
     address admin;



     /*====================*modifiers section start ====================*/

     modifier onlyAdmin() {
         require(msg.sender == admin, "Only admin allowed to call this function");
         _;
     }

     /*====================*Contract function section start ====================*/
     //@dev called when contract is created
     constructor() public {
         /**
          *@dev ensure that address is valid
          */
         require(msg.sender != address(0));
         admin = msg.sender;
     }


     /**
      *@dev responsible for adding a new student on the database
      * @param studentNo represents the student identification no
      * @notice empty variable checking is done outside of the blockchain
      */
     function addUser(string memory studentNo) public returns(bool) {
         require(msg.sender != address(0), "Invalid sender address in addUser function");
         Students[studentNo].studentNo = studentNo;
         Students[studentNo].usedCount = 0;
         Students[studentNo].active = true;
         emit addUserLogger(true);
         return true;
     }

     /**
      * @dev responsible for checking if a particular user exists
      * @param studentNo studentNo to verify * @notice empty variable checking is done outside of the blockchain
      * @return if exist or not
      *
      */
     function userExists(string memory studentNo) public returns(bool) {
         require(msg.sender != address(0), "Invalid sender address in function getUser");
         emit userExistsLogger(studentNo, Students[studentNo].active);
         return Students[studentNo].active;
     }



     /*====================Docking Station functions Section Start ====================*/

     /**
      * @dev responsible for adding a new docking station
      * * @notice empty variable checking is done outside of the blockchain
      */
     function registerDockingStation(string memory name, string memory latitude, string memory longitude) onlyAdmin public returns(bool) {
         /**
          * @dev ensure that the sender address is valid
          */
         require(msg.sender != address(0), "Invalid sender address");
         bytes32 hash = keccak256((abi.encode(name, latitude, longitude)));
         require(!dockingStations[hash].active, "Docking Station already exits");
         dockingStations[hash].id = hash;
         dockingStations[hash].dockingStationInformation.name = name;
         dockingStations[hash].dockingStationInformation.longitude = longitude;
         dockingStations[hash].dockingStationInformation.latitude = latitude;
         dockingStations[hash].count = 0;
         dockingStations[hash].active = true;
         dockingStationKeys.push(hash);
         emit addDockingStationLogger(hash, true);
         return true;
     }

     function getRegisteredDockingStationKeys() public view returns(bytes32[] memory) {
         require(msg.sender != address(0), "Invalid sender address");
         return dockingStationKeys;
     }
     /**
      *@dev responsible for checking if a docking is registered
      * @param name the DockingStation name to search for
      * @notice empty variable checking is done outside of the blockchain
      */
     function dockingStationExists(string memory name, string memory latitude, string memory longitude) onlyAdmin public
     view returns(bool) {
         require(msg.sender != address(0), "Invalid sender address");
         bytes32 hash = keccak256(abi.encode(name, latitude, longitude));
         return dockingStations[hash].active;
     }

     function getDockingStation(bytes32 id) public view returns(string memory name, string memory latitude, string memory longitude) {
         require(msg.sender != address(0), "Invalid sender address");
         name = dockingStations[id].dockingStationInformation.name;
         latitude = dockingStations[id].dockingStationInformation.latitude;
         longitude = dockingStations[id].dockingStationInformation.longitude;
     }


 }