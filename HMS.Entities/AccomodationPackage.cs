using System.Collections.Generic;

namespace HMS.Entities
{
    public class AccomodationPackage
    {
        public int ID { get; set; }
        public int AccomodationTypeId { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }
        public string PackageName { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public virtual List<PackagePicture> PackagePictures { get; set; }
        
        /*public string PackDesc { get; set; }
        Residency Apartment.
        Each room in the resort can accommodate 2 adults and 2 children.Perfect for traveling couples.Breakfast included.
        Price does not include VAT & services fee.
        We love to tell our successful far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts.
        */
    }
}
