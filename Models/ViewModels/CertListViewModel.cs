using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models.ViewModels
{
    public class CertListViewModel
    {
       public IEnumerable<Certificate> Certificates { get; set; }
    }
}
