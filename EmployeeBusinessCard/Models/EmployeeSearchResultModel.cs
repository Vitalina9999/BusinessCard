using EmployeeBusinessCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBusinessCard.Models
{
    public class EmployeeSearchResultViewModel
    {
        public IList<EmployeeViewModel> Employees { get; set; }
         
        public EmployeeSearchViewModel EmployeeSearchParameters { get; set; }
    }
}
