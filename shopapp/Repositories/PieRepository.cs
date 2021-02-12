﻿using Microsoft.EntityFrameworkCore;
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

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.AsNoTracking().FirstOrDefault(P => P.PieId == pieId);
        }
    }
}
