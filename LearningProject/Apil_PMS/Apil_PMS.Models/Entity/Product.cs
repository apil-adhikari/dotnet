using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Apil_PMS.Models.Entity
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Required]
        public float Rate { get; set; }

        [Required]
        public int BatchNo { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public IFormFile ProductImage { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvaliable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
