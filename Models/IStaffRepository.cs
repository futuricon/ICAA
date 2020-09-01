using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public interface IStaffRepository
    {
        IQueryable<Staff> Staffs { get; }
        void SaveStaff(Staff staff);
        Staff DeleteStaff(int staffID);
    }
}

