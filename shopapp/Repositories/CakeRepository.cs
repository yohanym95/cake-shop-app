using Microsoft.EntityFrameworkCore;
using shopapp.Data;
using shopapp.Models;
using shopapp.ViewModels;
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
        private readonly IPieRepository _pieRepository;

        public CakeRepository(AppDbContext appDbContext, IPieRepository pieRepository)
        {
            _appDbContext = appDbContext;
            _pieRepository = pieRepository;
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

        public bool ModifyCake(CakeViewModel cakeViewModel)
        {
            //  var pies = _appDbContext.Pies.Include(c => c.Category).FirstOrDefault(p => p.PieId == pie.PieId);
            if(cakeViewModel.PieViewModel.PieId != 0)
            {
                var pie = _appDbContext.Pies.FirstOrDefault(P => P.PieId == cakeViewModel.PieViewModel.PieId);

                pie.Name = cakeViewModel.PieViewModel.Name;
                pie.CategoryId = cakeViewModel.PieViewModel.CategoryId;
                
                pie.ShortDescription = cakeViewModel.PieViewModel.ShortDescription;
                pie.LongDescription = cakeViewModel.PieViewModel.LongDescription;
                pie.InStock = cakeViewModel.PieViewModel.InStock;
                pie.IsPieOfTheWeek = cakeViewModel.PieViewModel.IsPieOfTheModel;
                
                if(cakeViewModel.PieViewModel.PiePictureByte != null)
                {
                    pie.PictureByte = cakeViewModel.PieViewModel.PiePictureByte;
                }

                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool DeleteCake(int pieId)
        {
            var pie = _appDbContext.Pies.Find(pieId);
            var shoppingItems = _appDbContext.ShoppinCartItems.Where(o => o.Pie.PieId == pieId).ToList();
            _appDbContext.ShoppinCartItems.RemoveRange(shoppingItems);
            _appDbContext.Pies.Remove(pie);
            _appDbContext.SaveChanges();
            return true;
            
        }
    }
}
