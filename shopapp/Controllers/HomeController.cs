using Microsoft.AspNetCore.Mvc;
using shopapp.Models;
using shopapp.Repositories;
using shopapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            };

            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        public IActionResult Search(string search)
        {

            Debug.WriteLine(search);
            var pies = _pieRepository.GetSearchPiesSet(search).OrderBy(p => p.PieId);
            if (pies.Count() == 0)
                return RedirectToAction("Status","Home",new { message = "No Result Found"});

            var searchViewModel = new SearchViewModel
            {
                Pies = pies,
                Keyword = search,
                Count = pies.Count()
            };
                
            return View(searchViewModel);
      
        }

        public IActionResult Status(string message)
        {
            ViewBag.status = message;
            return View();
        }
    }
}
