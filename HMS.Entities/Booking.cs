using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Entities
{
    public class Booking
    {
        public int ID { get; set; }
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }
        public DateTime FromDate { get; set; }
        /// <summary>
        /// No Of Stay Nights
        /// </summary>
        public int Duration { get; set; }
        public string GuestName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChilds { get; set; }
        public bool CheckOut { get; set; }
        public string Notes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime CreatedOn { get; private set; }
    }
}
