using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class Gallery
    {
        [Key]
        public int ImageID  { get; set; }
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Please enter tags")]
        public string Info { get; set; }
        public DateTime UploadDate { get; set; }
        public ICollection<InfoTag> InfoTags { get; set; }
    }
}
