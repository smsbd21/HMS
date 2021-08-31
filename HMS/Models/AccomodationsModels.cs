using System;
using HMS.Entities;
using System.Collections.Generic;

namespace HMS.Models
{
    public class AvailabilityModel
    {
        public int PackId { get; set; }
        public string GuestName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChilds { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public bool CheckOut { get; set; }
        public string Notes { get; set; }
    }
    public class PackageBookingModel
    {
        public AccomodationPackage AccomodationPackage { get; set; }
    }
    public class PackageDetailsModel
    {
        public Accomodation Accomodation { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
    }
    public class AccomodationsViewModel
    {
        public int PackId { get; internal set; }
        public AccomodationType AccomodationType { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}