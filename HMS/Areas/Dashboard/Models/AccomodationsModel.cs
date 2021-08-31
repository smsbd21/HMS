using HMS.Models;
using HMS.Entities;
using System.Collections.Generic;

namespace HMS.Areas.Dashboard.Models
{
    public class AccomodationActionModel
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public string AccName { get; set; }
        public string Description { get; set; }
        public string PictureIds { get; set; } // "90,67,23"
        public List<AccomPicture> AccomPictures { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
    public class AccomodationsListingModel
    {
        public Pager Pager { get; set; }
        public string SrcAcom { get; set; }
        public int? AccPackId { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }    
}