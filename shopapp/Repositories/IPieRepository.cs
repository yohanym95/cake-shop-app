using shopapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Repositories
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }
        Pie GetPieById(int pieId);
        IEnumerable<string> GetSearchPies(string search);

        IEnumerable<Pie> GetSearchPiesSet(string pieName);
    }
}
