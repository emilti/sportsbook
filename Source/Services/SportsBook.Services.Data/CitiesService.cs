namespace SportsBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using SportsBook.Data.Common;
    using SportsBook.Data.Models;

    public class CitiesService : ICitiesService
    {
        private readonly IRepository<City> cities;

        public CitiesService(
            IRepository<City> citiesRepo)
        {
            this.cities = citiesRepo;
        }

        public IQueryable<City> All()
        {
            return this.cities.All();
        }

        public City GetById(int cityId)
        {
            return this.cities.GetById(cityId);
        }
    }
}
