using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Models
{
    public class MockCateforyRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories => new List<Category>
        {
            new Category{CategoryId=1, CategoryName="Fruit pies", Description="All-Fruit pies"},
            new Category{CategoryId=2, CategoryName="Cheese cakes", Description="Cheesy all the way"},
            new Category{CategoryId=3, CategoryName="Seasonal pies", Description="Get in the mood for seasonal pie"}
        };


    }
}
