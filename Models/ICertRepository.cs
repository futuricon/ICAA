using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public interface ICertRepository
    {
        IQueryable<Certificate> Certificates { get; }
        void SaveCert(Certificate Cert);
        Certificate DeleteCert(int certId);
    }
}

