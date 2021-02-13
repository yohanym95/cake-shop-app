using Microsoft.AspNetCore.Mvc;
using shopapp.Repositories;
using System.Linq;

namespace shopapp.Components
{
    public class CategoryButtons : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryButtons(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
