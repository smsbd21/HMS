using HMS.Models;
using HMS.Entities;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace HMS.Areas.Dashboard.Models
{
    public class UserRolesModel
    {
        public string UserId { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<IdentityRole> UserRoles { get; set; }
    }
    public class UserActionModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        //ID,FullName,Email,UserName,Country,City,Address
    }
    public class UsersListingModel
    {
        public Pager Pager { get; set; }
        public string RoleId { get; set; }
        public string SrcUser { get; set; }
        public IEnumerable<HMSUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}