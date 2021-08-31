using HMS.Entities;
using System.Collections.Generic;

namespace HMS.Areas.Dashboard.Models
{
    public class AccomodationTypeActionModel
    {
        public int ID { get; set; }
        public string AccType { get; set; }
        public string Description { get; set; }
    }
    public class AccomodationTypesListingModel
    {
        public string SrcType { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    }
}