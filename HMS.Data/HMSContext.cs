using HMS.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Data
{
    public class HMSContext : IdentityDbContext<HMSUser>
    {
        public HMSContext() : base("DBConStr")
        {
            // Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=HMSDB;
        }

        // public int iTotalRec = 0;
        public static HMSContext Create()
        {
            return new HMSContext();
        }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<PackagePicture> PackagePictures { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<AccomPicture> AccomPictures { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
