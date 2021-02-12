using Microsoft.EntityFrameworkCore;
using shopapp.Data;
using shopapp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly AppDbContext _appDbContext;

        public CakeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public  bool CreateCake(Pie pie)
        {
            var pies = _appDbContext.Pies.Include(c => c.Category).Where(p => p.Name == pie.Name).Where(p => p.CategoryId == pie.CategoryId).ToList();

            if(pies.Count  <= 0)
            {
                _appDbContext.Pies.Add(pie);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModifyCake(Pie pie)
        {
            //  var pies = _appDbContext.Pies.Include(c => c.Category).FirstOrDefault(p => p.PieId == pie.PieId);
            if(pie != null)
            {
                _appDbContext.Pies.Update(pie);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
