using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class EFCertRepository : ICertRepository
    {
        private AppIdentityDbContext context;
        public EFCertRepository(AppIdentityDbContext ctx) { context = ctx; }
        public IQueryable<Certificate> Certificates => context.Certificates;
        public void SaveCert(Certificate cert)
        {
            if (cert.CertID == 0)
            {
                context.Certificates.Add(cert);
            }
            else
            {
                Certificate dbEntry = context.Certificates
                    .FirstOrDefault(p => p.CertID == cert.CertID);
                if (dbEntry != null)
                {
                    if (cert.CertPath != null)
                    {
                        dbEntry.CertPath = cert.CertPath;
                    }
                    if (cert.StaffId != 0)
                    {
                        dbEntry.StaffId = cert.StaffId;
                    }
                    if (cert.CertName != null)
                    {
                        dbEntry.CertName = cert.CertName;
                    }
                }
            }
            context.SaveChanges();
        }
        public Certificate DeleteCert(int certId)
        {
            Certificate dbEntry = context.Certificates
                    .FirstOrDefault(p => p.CertID == certId);
            if (dbEntry != null)
            {
                context.Certificates.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        //public Certificate RenameCert(int certId)
        //{
        //    Certificate dbEntry = context.Certificates
        //            .FirstOrDefault(p => p.CertID == certId);
        //    if (dbEntry != null)
        //    {
        //        dbEntry.CertName = cer
        //        context.SaveChanges();
        //    }
        //    return dbEntry;
        //}
    }
}
