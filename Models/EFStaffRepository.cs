using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class EFStaffRepository : IStaffRepository
    {
        private AppIdentityDbContext context;
        public EFStaffRepository(AppIdentityDbContext ctx) { context = ctx; }
        public IQueryable<Staff> Staffs => context.Staffs.Include(x => x.Certificates);
        public void SaveStaff(Staff staff)
        {
            if (staff.StaffID == 0)
            {
                context.Staffs.Add(staff);
            }
            else
            {
                Staff dbEntry = context.Staffs
                    .FirstOrDefault(p => p.StaffID == staff.StaffID);
                if (dbEntry != null)
                {
                    dbEntry.Name = staff.Name;
                    dbEntry.Surname = staff.Surname;
                    dbEntry.FullInfo = staff.FullInfo;
                    dbEntry.Description = staff.Description;
                    dbEntry.NameRu = staff.NameRu;
                    dbEntry.SurnameRu = staff.SurnameRu;
                    dbEntry.FullInfoRu = staff.FullInfoRu;
                    dbEntry.DescriptionRu = staff.DescriptionRu;
                    dbEntry.NameUz = staff.NameUz;
                    dbEntry.SurnameUz = staff.SurnameUz;
                    dbEntry.FullInfoUz = staff.FullInfoUz;
                    dbEntry.DescriptionUz = staff.DescriptionUz;
                    if (staff.ImageUrl != null)
                    {
                        dbEntry.ImageUrl = staff.ImageUrl;
                    }
                    if (staff.Certificates != null)
                    {
                        dbEntry.Certificates = staff.Certificates;
                    }
                }
            }
            context.SaveChanges();
        }
        public Staff DeleteStaff(int staffId)
        {
            Staff dbEntry = context.Staffs
                    .FirstOrDefault(p => p.StaffID == staffId);
            if (dbEntry != null)
            {
                context.Staffs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
