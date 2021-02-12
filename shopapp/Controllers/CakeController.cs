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
        private readonly ICategoryRepository _categoryRepository;

        public CakeController(ICakeRepository cakeRepository, IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _cakeRepository = cakeRepository;
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
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
                    Notes = pie.Notes,
                    CategoryId = pie.CategoryId
                    

                };

            }
            else
            {
                return Index();
            }

            var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            CakeViewModel cakeViewModel = new CakeViewModel();
            cakeViewModel.Categories = categories;

            cakeViewModel.PieViewModel = pieModel;

            return View(cakeViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Manage(CakeViewModel cakeViewModel)
        {
            if (ModelState.IsValid)
            {
                var pieDetails = _pieRepository.GetPieById(cakeViewModel.PieViewModel.PieId);
                if (Request.Form.Files.Count >0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();

                    if(file != null)
                    {
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            var picBytes = dataStream.ToArray();
                            cakeViewModel.PieViewModel.PiePictureByte = picBytes;
                           
                        }
                    }                
                }

                var result = _cakeRepository.ModifyCake(cakeViewModel);
                if (!result)
                {
                    ModelState.AddModelError("", "Try again Later");
                }
                else
                {
                    return RedirectToAction("AddingComplete");
                }
            }
            return View();
        }

        public ViewResult Add()
        {
            var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);

            CakeViewModel cakeViewModel = new CakeViewModel();
            cakeViewModel.Categories = categories;

            return View(cakeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CakeViewModel cakeViewModel) 
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        cakeViewModel.PieViewModel.PiePictureByte = dataStream.ToArray();
                    }

                    var pie = new Pie
                    {
                        Name = cakeViewModel.PieViewModel.Name,
                        ShortDescription = cakeViewModel.PieViewModel.ShortDescription,
                        LongDescription = cakeViewModel.PieViewModel.LongDescription,
                        Price = cakeViewModel.PieViewModel.Price,
                        PictureByte = cakeViewModel.PieViewModel.PiePictureByte,
                        IsPieOfTheWeek = cakeViewModel.PieViewModel.IsPieOfTheModel,
                        InStock = cakeViewModel.PieViewModel.InStock,
                        CategoryId = cakeViewModel.PieViewModel.CategoryId

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
            return View(cakeViewModel);
        }

        public IActionResult AddingComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Successfully Added the Pies!";
            return View();
        }
    }
}
