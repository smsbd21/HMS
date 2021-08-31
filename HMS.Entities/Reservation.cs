using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.Entities
{
    //[Table("Tb_Reservation")]
    public class Reservation
    {
        [Key]
        public int RsvId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string Sex { get; set; }
        public string CellNo { get; set; }
        public string Email { get; set; }
        public int NoOfGuest { get; set; }
        public string Street { get; set; }
        public string AptSuite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string RoomType { get; set; }
        public string FloorNo { get; set; }
        public string RoomNo { get; set; }
        public decimal TotalBill { get; set; }
        public string PaymentType { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string CardExp { get; set; }
        public string CardCvc { get; set; }
        public bool CheckIn { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime LeavingTime { get; set; }
        public int BreakFast { get; set; }
        public int Lunch { get; set; }
        public int Dinner { get; set; }
        public bool Cleaning { get; set; }
        public bool Towel { get; set; }
        public bool SpSurprise { get; set; }
        public bool SupplyStatus { get; set; }
        public int FoodBill { get; set; }
    }
}
