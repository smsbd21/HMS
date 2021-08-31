using System.Collections.Generic;

namespace HMS.Entities
{
    public class Accomodation
    {
        public int ID { get; set; }
        public int AccomodationPackageId { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }
        public string AccName { get; set; }
        public string Description { get; set; }
        public virtual List<AccomPicture> AccomPictures { get; set; }
    }
}
