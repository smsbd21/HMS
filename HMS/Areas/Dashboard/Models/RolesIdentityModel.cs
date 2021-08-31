using HMS.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Areas.Dashboard.Models
{
    public class RoleActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class RolesListingModel
    {
        public Pager Pager { get; set; }
        public string SrcRole { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}