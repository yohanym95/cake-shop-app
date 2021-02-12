using shopapp.Models;
using shopapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.Repositories
{
    public interface ICakeRepository
    {
        bool CreateCake(Pie pie);

        bool ModifyCake(CakeViewModel cakeViewModel);

        bool DeleteCake(int pieId);
    }
}
