using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.ViewModels
{
    public class PieViewModel
    {
        public int PieId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = " Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Pie Picture")]
        public byte[] PiePictureByte { get; set; }
        [Display(Name = "Pie Picture")]
        public string PiePicture { get; set; }
        [Display(Name ="Is it pie of the week?")]
        public bool IsPieOfTheModel { get; set; }
        [Display(Name="Is it Available in stock?")]
        public bool InStock { get; set; }
        [Display(Name="Notes")]
        public string Notes { get; set; }
    }
}
