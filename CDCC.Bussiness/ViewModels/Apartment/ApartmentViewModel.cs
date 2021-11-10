using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels.Apartment
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
    }
}
