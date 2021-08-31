using HMS.Data;
using System.Linq;
using HMS.Entities;
using System.Collections.Generic;

namespace HMS.Services
{
    public class DashboardService
    {
        public bool SavePicture(Picture oPic)
        {
            var context = new HMSContext();
            context.Pictures.Add(oPic);
            return context.SaveChanges() > 0;
        }
        public IEnumerable<Picture> GetPictureByIds(List<int> iPic)
        {
            var context = new HMSContext();
            return iPic.Select(x => context.Pictures.Find(x)).ToList();
        }
    }
}
