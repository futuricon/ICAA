using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class Certificate
    {
        [Key]
        public int CertID { get; set; }
        public string CertPath { get; set; }

        public string CertName { get; set; }

        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}
