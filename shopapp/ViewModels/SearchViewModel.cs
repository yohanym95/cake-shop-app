using shopapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public string Keyword { get; set; }
        public int Count { get; set; }
        
    }
}
