using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using shopapp.Models;
using shopapp.Repositories;
using shopapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly IPieRepository _pieRepository;

        public CakeController(ICakeRepository cakeRepository, IPieRepository pieRepository)
        {
            _cakeRepository = cakeRepository;
            _pieRepository = pieRepository;
        }
        public ViewResult Index()
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
            currentCategory = "All pies";

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult AddPies(Pie pie)
        {
            if (ModelState.IsValid)
            {
                _cakeRepository.CreateCake(pie);
                return RedirectToAction("AddComplete");
            }
            return View(pie);
        }

        public IActionResult AddComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Successfully Added the pie";
            return View();
        }

        public ViewResult List()
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
            currentCategory = "All pies";

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Manage(int pieId)
        {
            var pie = _pieRepository.GetPieById(pieId);
            PieViewModel pieModel;
            if (pie.PieId != 0)
            {
                pieModel = new PieViewModel
                {
                    PieId = pie.PieId,
                    Name = pie.Name,
                    ShortDescription = pie.ShortDescription,
                    LongDescription = pie.LongDescription,
                    Price = pie.Price,
                    PiePicture = pie.ImageUrl,
                    PiePictureByte = pie.PictureByte,
                    IsPieOfTheModel = pie.IsPieOfTheWeek,
                    InStock = pie.InStock,
                    Notes = pie.Notes

                };
                Debug.WriteLine(pie.PieId);

            }
            else
            {
                return Index();
            }

            return View(pieModel);

        }

        [HttpPost]
        public IActionResult Manage(PieViewModel pieViewModel)
        {
            Debug.Write(pieViewModel.PieId);
            if (ModelState.IsValid)
            {
                var pie = new Pie
                {
                    PieId = pieViewModel.PieId,
                    Name = pieViewModel.Name,
                    Price = pieViewModel.Price,
                    ShortDescription = pieViewModel.ShortDescription,
                    LongDescription = pieViewModel.LongDescription,
                    InStock = pieViewModel.InStock,
                    IsPieOfTheWeek = pieViewModel.IsPieOfTheModel,
                    PictureByte = pieViewModel.PiePictureByte,
                    ImageUrl = pieViewModel.PiePicture
                };

                Debug.WriteLine(pie.PieId);
                var result = _cakeRepository.ModifyCake(pie);

                if (!result)
                {
                    ModelState.AddModelError("","Try again Later");
                }
                else
                {
                    return RedirectToAction("AddingComplete");
                }
            }
            return View(pieViewModel);
        }

        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PieViewModel pieView) 
        {

            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        pieView.PiePictureByte = dataStream.ToArray();
                    }

                    var pie = new Pie
                    {
                        Name = pieView.Name,
                        ShortDescription = pieView.ShortDescription,
                        LongDescription = pieView.LongDescription,
                        Price = pieView.Price,
                        PictureByte = pieView.PiePictureByte,
                        IsPieOfTheWeek = pieView.IsPieOfTheModel,
                        InStock = pieView.InStock,
                        CategoryId = 1

                    };

                    var result =  _cakeRepository.CreateCake(pie);
                    if (result)
                    {
                        return RedirectToAction("AddingComplete");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Already There is a pie with same name ");
                    }
                    
                }

            }
            return View(pieView);
        }

        public IActionResult AddingComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Successfully Added the Pies!";
            return View();
        }
    }
}
