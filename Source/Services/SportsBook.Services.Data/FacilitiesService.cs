﻿namespace SportsBook.Services.Data
{
    using System;
    using System.Linq;
    using SportsBook.Data.Common;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;

    public class FacilitiesService : IFacilitiesService
    {
        private readonly IDbRepository<Facility> facilities;

        public FacilitiesService(IDbRepository<Facility> facilities)
        {
            this.facilities = facilities;
        }

        public Facility Add(string content, string authorId, string username, Facility commentedFacility)
        {
            // var newComment = new FacilityComment
            // {
            //     FacilityId = facilityId,
            //     Content = content,
            //     AuthorName = username,
            //     AuthorId = authorId,
            //     CreatedOn = DateTime.UtcNow,
            // }; 
            //
            return new Facility();
        }

        public IQueryable<AppUser> All(int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Facility> GetTopFacilities()
        {
            return this.facilities.All().OrderByDescending(x => x.CreatedOn).Take(6);
        }

        public Facility GetFacilityDetails(int id)
        {
            return this.facilities.GetById(id);
        }

        public void UpdateFacility()
        {
            this.facilities.Save();
        }

        public void Add(Facility facility)
        {
            this.facilities.Add(facility);
            this.facilities.Save();
        }
    }
}
