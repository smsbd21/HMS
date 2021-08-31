using HMS.Entities;
using System.Collections.Generic;

namespace HMS.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}