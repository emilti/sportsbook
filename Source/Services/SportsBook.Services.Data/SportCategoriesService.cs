using SportsBook.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsBook.Data.Models;
using SportsBook.Data.Common;

namespace SportsBook.Services.Data
{
    public class SportCategoriesService : ISportCategoriesService
    {

        private readonly IDbRepository<SportCategory> sportCategories;

        public SportCategoriesService(
            IDbRepository<SportCategory> sportCategoriesRepo)
        {
            this.sportCategories = sportCategoriesRepo;
        }

        public IQueryable<SportCategory> All()
        {
            return this.sportCategories.All();
        }

        public SportCategory GetById(int sportCategoryId)
        {
            SportCategory foundSportCategory = this.sportCategories.GetById(sportCategoryId);
            return foundSportCategory;
        }
    }
}
