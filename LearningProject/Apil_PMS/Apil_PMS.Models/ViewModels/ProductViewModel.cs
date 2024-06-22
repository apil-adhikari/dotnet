using Apil_PMS.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apil_PMS.Models.ViewModels
{
    public class ProductViewModel
    {
        public SearchViewModel searchViewModel { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}
