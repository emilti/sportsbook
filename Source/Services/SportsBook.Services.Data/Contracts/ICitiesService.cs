﻿namespace SportsBook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsBook.Data.Models;

    public interface ICitiesService
    {
        IQueryable<City> All();

        City GetById(int cityId);
    }
}
