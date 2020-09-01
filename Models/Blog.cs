using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace ICAA.Models
{
    public class Blog
    {
        public int BlogID { get; set; }

        public string ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Please enter a blog title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a blog title")]
        public string TitleRu { get; set; }

        [Required(ErrorMessage = "Please enter a blog title")]
        public string TitleUz { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string DescriptionRu { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string DescriptionUz { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
