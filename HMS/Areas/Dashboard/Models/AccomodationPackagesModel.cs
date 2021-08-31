using HMS.Models;
using HMS.Entities;
using System.Collections.Generic;

namespace HMS.Areas.Dashboard.Models
{
    public class AccomodationPackageActionModel
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public string PackageName { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string PictureIds { get; set; } // "90,67,23"
        public List<PackagePicture> PackagePictures { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    }
    public class AccomodationPackagesListingModel
    {
        public Pager Pager { get; set; }
        public string SrcPack { get; set; }
        public int? AccTypeId { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}