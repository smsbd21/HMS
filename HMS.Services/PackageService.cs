using HMS.Data;
using System.Linq;
using HMS.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace HMS.Services
{
    public class AccomodationPackageService
    {
        public int iRecord = 5;
        public int iTotalRec = 5;
        public int GetTotalPackage(string srcVal, int? iType)
        {
            var context = new HMSContext();
            var oPack = context.AccomodationPackages.AsQueryable();
            if (iType.HasValue && iType.Value > 0) oPack = oPack.Where(x => x.AccomodationTypeId.Equals(iType.Value));
            if (!string.IsNullOrEmpty(srcVal)) oPack = oPack.Where(x => x.PackageName.ToLower().Contains(srcVal.ToLower()));
            return oPack.Count();
        }
        public List<PackagePicture> GetPicturesByPackId(int iPack)
        {
            var context = new HMSContext();
            return context.AccomodationPackages.Find(iPack).PackagePictures.ToList();
        }
        public AccomodationPackage GetAccomodationPackageById(int id)
        {
            // A common cause of this error is disposing a context
            // That was resolved from dependency injection
            var context = new HMSContext();
            return context.AccomodationPackages.Find(id);
        }
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackage()
        {
            var context = new HMSContext();
            return context.AccomodationPackages.ToList();
        }
        public IEnumerable<AccomodationPackage> GetPackagesByTypeId(int iType)
        {
            var context = new HMSContext();
            return context.AccomodationPackages.Where(x => x.AccomodationTypeId == iType).ToList();
        }
        public bool SetAccomodationPackage(AccomodationPackage oPack, string strKey)
        {
            var context = new HMSContext();
            if (strKey == "Save") context.AccomodationPackages.Add(oPack);
            else
            {
                var oExPack = context.AccomodationPackages.Find(oPack.ID);
                context.PackagePictures.RemoveRange(oExPack.PackagePictures);
                if (strKey == "Edit")
                {
                    context.Entry(oExPack).CurrentValues.SetValues(oPack);
                    context.PackagePictures.AddRange(oPack.PackagePictures);
                }
                else context.Entry(oExPack).State = EntityState.Deleted;
            }
            return context.SaveChanges() > 0;
            //else if (strKey == "Delete")
            //{
            //    //context.AccomodationPackages.Attach(oPack);
            //    //context.Set<AccomodationPackage>().Remove(oPack);
            //    context.Entry(oPack).State = EntityState.Detached;
            //    context.Entry(oPack).State = EntityState.Deleted;
            //}
        }
        public IEnumerable<AccomodationPackage> GetAccomodationPackages(string sPack, int? iType, int iReco, int iCurPage)
        {
            var context = new HMSContext();
            var oPack = context.AccomodationPackages.AsQueryable();
            if (iType.HasValue && iType.Value > 0) oPack = oPack.Where(x => x.AccomodationTypeId.Equals(iType.Value));
            if (!string.IsNullOrEmpty(sPack)) oPack = oPack.Where(x => x.PackageName.ToLower().Contains(sPack.ToLower()));

            // Showing pagination to indicate a series of related content exists across multiple pages.
            iTotalRec = oPack.Count();
            // Working pagination with icons
            int iSkip = iReco * (iCurPage - 1);
            //  iSkip = (1 - 1) = 0 * 3 = 0
            //  iSkip = (2 - 1) = 1 * 3 = 3
            //  iSkip = (3 - 1) = 2 * 3 = 6

            return oPack.OrderBy(x=>x.AccomodationTypeId).Skip(iSkip).Take(iReco).ToList();
        }
    }
}
