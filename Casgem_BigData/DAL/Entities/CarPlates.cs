namespace Casgem_BigData.DAL.Entities
{
    public class CarPlates
    {
        public int CarPlateID { get; set; }
        public int CarID { get; set; }
        public string PLATE { get; set; }
        public DateTime LicenceDate { get; set; }
        public int CITYNR { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Fuel { get; set; }
        public string ShiftType { get; set; }
        public string MotorVolume { get; set; }
        public string MotorPower { get; set; }
        public string Color { get; set; }
        public string CASETYPE { get; set; }
    }
}
