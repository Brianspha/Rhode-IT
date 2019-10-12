using Realms;

namespace RhodeIT.Models
{
    public class BicycleRental : RealmObject
    {
        public string BicycleID { get; set; }
        public string DockingStation { get; set; }
        public string Ethereum_Address { get; set; }
    }
}
