using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apil_PMS.Models.Entity
{
    public class Category:BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryDescription { get; set; }
        [Required]
        public bool IsAvaliable { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } 

        // Relationship
        // one category has multiple product
        public virtual ICollection<Product> Products { get; set; }
    }
}
