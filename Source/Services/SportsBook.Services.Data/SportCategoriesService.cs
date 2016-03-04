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
        private readonly IRepository<SportCategory> sportCategories;

        public SportCategoriesService(
            IRepository<SportCategory> sportCategoriesRepo)
        {
            this.sportCategories = sportCategoriesRepo;
        }

        public void Add(SportCategory sportCategory)
        {
            this.sportCategories.Add(sportCategory);
            this.sportCategories.SaveChanges();
        }

        public IQueryable<SportCategory> All()
        {
            return this.sportCategories.All();
        }

        public void Dispose()
        {
            this.sportCategories.Dispose();
        }

        public SportCategory GetById(int sportCategoryId)
        {
            SportCategory foundSportCategory = this.sportCategories.GetById(sportCategoryId);
            return foundSportCategory;
        }

        public void Remove(SportCategory sportCategory)
        {
            this.sportCategories.Delete(sportCategory.Id);
        }

        public void Save()
        {
            this.sportCategories.SaveChanges();
        }

        public void UpdateSportCategory(int id, SportCategory sportCategory)
        {
            SportCategory sportCategoryToUpdate = this.sportCategories.GetById(id);
            sportCategoryToUpdate.Name = sportCategory.Name;
            sportCategoryToUpdate.Description = sportCategory.Description;
            this.sportCategories.SaveChanges();
        }
    }
}
