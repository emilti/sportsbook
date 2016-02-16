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
        private readonly IDbRepository<City> cities;

        public CitiesService(
            IDbRepository<City> citiesRepo)
        {
            this.cities = citiesRepo;
        }

        public IQueryable<City> All()
        {
            return this.cities.All();
        }
    }
}
