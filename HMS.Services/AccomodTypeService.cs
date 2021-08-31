using HMS.Data;
using System.Linq;
using HMS.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace HMS.Services
{
    public class AccomodationTypeService
    {
        public AccomodationType GetAccomodationTypeById(int id)
        {
            var context = new HMSContext();
            return context.AccomodationTypes.Find(id);
        }        
        public bool SaveAccomodationType(AccomodationType oType)
        {
            var context = new HMSContext();
            context.AccomodationTypes.Add(oType);
            return context.SaveChanges() > 0;
        }
        public bool EditAccomodationType(AccomodationType oType)
        {
            var context = new HMSContext();
            context.Entry(oType).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }
        public bool DeleteAccomodationType(AccomodationType oType)
        {
            var context = new HMSContext();
            context.Entry(oType).State = EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
        public bool SetAccomodationType(AccomodationType oType, string strKey)
        {
            var context = new HMSContext();
            if (strKey == "Save") context.AccomodationTypes.Add(oType);
            else if (strKey == "Edit") context.Entry(oType).State = EntityState.Modified;
            else context.Entry(oType).State = EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
        public IEnumerable<AccomodationType> GetAccomodationTypes(string sType)
        {
            var context = new HMSContext();
            var oType = context.AccomodationTypes.AsQueryable();
            if (!string.IsNullOrEmpty(sType)) oType = oType.Where(x => x.AccType.ToLower().Contains(sType.ToLower()));
            return oType.ToList();
        }
    }
}