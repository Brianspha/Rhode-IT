pragma solidity ^ 0.4 .25;

/**
 *@Contract to help in the running of the Bicycle Sharing app
 */
contract RhodeIT {

struct Student{
    string studentNo;
    string password;
    mapping(uint256 => Bicycle) Bicycles;
}
struct Bicycle{
    uint256 ID;
}

}