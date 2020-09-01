using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models.ViewModels
{
    public class StaffListViewModel
    {
        public IEnumerable<Staff> Staffs { get; set; }
        //public StaffPagingInfo StaffPagingInfo { get; set; }
        public string PersonId { get; set; }

        public IEnumerable<Gallery> Galleries { get; set; }
    }
}
