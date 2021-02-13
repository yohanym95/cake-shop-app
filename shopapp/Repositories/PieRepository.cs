using Microsoft.EntityFrameworkCore;
using shopapp.Data;
using shopapp.Models;
using shopapp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public IEnumerable<string> GetSearchPies(string search)
        {
            var pies = _appDbContext.Pies.Where(p => p.Name.Contains(search)).Select(p => p.Name).ToList();
            return pies;
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.AsNoTracking().FirstOrDefault(P => P.PieId == pieId);
        }

        public IEnumerable<Pie> GetSearchPiesSet(string search)
        {
            var pies = _appDbContext.Pies.Include(c => c.Category).Where(p => p.Name.ToLower().Contains(search.ToLower()));
            return pies;
        }
    }
}
