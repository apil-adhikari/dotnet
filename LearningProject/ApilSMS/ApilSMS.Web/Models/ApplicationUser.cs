using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApilSMS.Web.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage ="First Name is require")]
        [StringLength(100)]
        public string FristName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage ="Last Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="User Role Id is required")]
        public string UserRoleId { get; set; }
        [Url(ErrorMessage ="ProfileURL must have a valid URL")]
        public string ProfileUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
