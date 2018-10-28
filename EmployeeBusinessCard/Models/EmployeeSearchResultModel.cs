using EmployeeBusinessCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBusinessCard.Models
{
    public class EmployeeSearchResultModel
    {
        public IList<EmployeeViewModel> Employees { get; set; }
         
        public EmployeeViewModel EmployeeSearchParameters { get; set; }
    }
}
