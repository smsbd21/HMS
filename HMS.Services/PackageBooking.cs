using System;
using HMS.Data;
using System.Linq;
using HMS.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace HMS.Services
{
    public class PackageBookingService
    {
        public Booking GetBookingById(int id)
        {
            // A common cause of this error is disposing a context
            // That was resolved from dependency injection
            var context = new HMSContext();
            return context.Bookings.Find(id);
        }
        public IEnumerable<Booking> GetBookings()
        {
            var context = new HMSContext();
            return context.Bookings.ToList();
        }
        public bool BookingAction(Booking oAcom, string strKey)
        {
            var context = new HMSContext();
            if (strKey == "Save") context.Bookings.Add(oAcom);
            else if (strKey == "Edit") context.Entry(oAcom).State = EntityState.Modified;
            else context.Entry(oAcom).State = EntityState.Deleted;
            return context.SaveChanges() > 0;
        }

        public IEnumerable<Accomodation> GetAccomodations()
        {
            var context = new HMSContext();
            var oBook = context.Bookings.AsQueryable();            
            if (string.IsNullOrEmpty(oBook.FirstOrDefault().GuestName)) return context.Accomodations.ToList();
            else
            {
                oBook = oBook.Where(x => x.CheckOut.Equals("N")).AsQueryable();
                oBook = oBook.Where(x => x.FromDate >= DateTime.Now.Date).AsQueryable();
                return context.Accomodations.Where(x => !x.ID.Equals(oBook.Select(y => y.ID))).ToList();
            }

            //var oBook = context.Bookings.Where(x => x.FromDate >= DateTime.Now.Date && x.CheckOut.Equals("N")).AsQueryable();
            //return context.Accomodations.Where(x => !x.ID.Equals(oBook.Select(y => y.ID))).ToList();
            //var oRsv = context.Bookings.SqlQuery("Select AccomodationId as AccId From Booking Where FromDate >= '" + DateTime.Now.Date + "' And CheckOut='N'").ToList();


            //this will throw an exception
            //var oAcom = context.Accomodations.AsQueryable();
            //if (!string.IsNullOrEmpty(sAcom)) oAcom = oAcom.Where(x => x.AccName.ToLower().Contains(sAcom.ToLower()));
            //if (iPack.HasValue && iPack.Value > 0) oAcom = oAcom.Where(x => x.AccomodationPackageId.Equals(iPack.Value));
        }
        public IEnumerable<Accomodation> GetAccomodationsByPackId(int iPack)
        {
            var context = new HMSContext();
            return context.Accomodations.Where(x => x.AccomodationPackageId == iPack).ToList();

            /*
             * //Querying with LINQ to Entities 
                using (var context = new SchoolDBEntities())
                {
                    var query = context.Students
                                       .where(s => s.StudentName == "Bill")
                                       .FirstOrDefault<Student>();
                }

                //LINQ Query syntax:
                using (var context = new SchoolDBEntities())
                {
                    var query = from st in context.Students
                                where st.StudentName == "Bill"
                                select st;
   
                    var student = query.FirstOrDefault<Student>();
                }
             */
        }
    }
}
