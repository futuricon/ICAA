using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class Staff
    {
        public int StaffID { get; set; }

        
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string NameRu { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string NameUz { get; set; }

        [Required(ErrorMessage = "Please enter a surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter a surname")]
        public string SurnameRu { get; set; }

        [Required(ErrorMessage = "Please enter a surname")]
        public string SurnameUz { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string DescriptionRu { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string DescriptionUz { get; set; }

        [Required(ErrorMessage = "Please enter full information")]
        public string FullInfo { get; set; }

        [Required(ErrorMessage = "Please enter full information")]
        public string FullInfoRu { get; set; }

        [Required(ErrorMessage = "Please enter full information")]
        public string FullInfoUz { get; set; }

        public ICollection<Certificate> Certificates { get; set; }
    }
}
