using shopapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.ViewModels
{
    public class CakeViewModel
    {
        public PieViewModel PieViewModel { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
