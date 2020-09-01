using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class InfoTag
    {
        [Key]
        public int id { get; set; }
        public string TagName { get; set; }

        [ForeignKey("Gallery")]
        public int ImageID { get; set; }
        public Gallery Gallery { get; set; }
    }
}
