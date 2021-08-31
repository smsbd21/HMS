using HMS.Data;
using System.Linq;
using HMS.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace HMS.Services
{
    public class AccomodationService
    {
        public int iRecord = 5;
        public int iTotalRec = 0;
        public Accomodation GetAccomodationById(int id)
        {
            // A common cause of this error is disposing a context
            // That was resolved from dependency injection
            var context = new HMSContext();
            return context.Accomodations.Find(id);
        }
        public List<AccomPicture> GetPicturesByAcomId(int iAcom)
        {
            var context = new HMSContext();
            return context.Accomodations.Find(iAcom).AccomPictures.ToList();
        }
        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            var context = new HMSContext();
            return context.Accomodations.ToList();
        }
        public bool SetAccomodation(Accomodation oAcom, string strKey)
        {
            var context = new HMSContext();
            if (strKey == "Save") context.Accomodations.Add(oAcom);
            else
            {
                var oExAcom = context.Accomodations.Find(oAcom.ID);
                context.AccomPictures.RemoveRange(oExAcom.AccomPictures);
                if (strKey == "Edit")
                {
                    context.Entry(oExAcom).CurrentValues.SetValues(oAcom);
                    context.AccomPictures.AddRange(oAcom.AccomPictures);
                }
                else context.Entry(oExAcom).State = EntityState.Deleted;
            }
            return context.SaveChanges() > 0;
        }
        public IEnumerable<Accomodation> GetAccomodationsByPackId(int iPack)
        {
            var context = new HMSContext();
            return context.Accomodations.Where(x => x.AccomodationPackageId == iPack).ToList();
        }
        public IEnumerable<Accomodation> GetAccomodations(string sAcom, int? iPack, int iReco, int iCurPage)
        {
            var context = new HMSContext();
            var oAcom = context.Accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(sAcom)) oAcom = oAcom.Where(x => x.AccName.ToLower().Contains(sAcom.ToLower()));
            if (iPack.HasValue && iPack.Value > 0) oAcom = oAcom.Where(x => x.AccomodationPackageId.Equals(iPack.Value));

            // Showing pagination
            iTotalRec = oAcom.Count();
            // Working pagination with icons
            int iSkip = iReco * (iCurPage - 1);

            return oAcom.OrderBy(x => x.AccomodationPackageId).Skip(iSkip).Take(iReco).ToList();
        }
    }
}
