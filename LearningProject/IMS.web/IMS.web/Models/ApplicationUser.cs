using Microsoft.AspNetCore.Identity;

namespace IMS.web.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Fields that a user has
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        //public string UserName { get; set; }

        // Department Info:Using StoreId
        public int StoreId { get; set; }


        // public string PhoneNumber { get; set; }

        // Generally to identify user type
        public string UserRoleId { get; set; }

        //
        public bool IsActive { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        // ---also add other parms




    }
}
