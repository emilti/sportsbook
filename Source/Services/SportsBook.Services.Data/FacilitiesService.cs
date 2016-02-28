namespace SportsBook.Services.Data
{
    using System;
    using System.Linq;
    using SportsBook.Data.Common;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;

    public class FacilitiesService : IFacilitiesService
    {
        private readonly IRepository<Facility> facilities;

        public FacilitiesService(IRepository<Facility> facilities)
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

        public IQueryable<Facility> All(int page = 1, int pageSize = 10)
        {
            return this.facilities.All();
        }

        public IQueryable<Facility> GetTopFacilities()
        {
            return this.facilities.All().OrderByDescending(x => x.CreatedOn).Take(6);
        }

        public Facility GetFacilityDetails(int id)
        {
            return this.facilities.GetById(id);
        }

        public void UpdateFacility(int id, Facility facility)
        {
            Facility facilityToUpdate = this.facilities.GetById(id);
            facilityToUpdate.CityId = facility.CityId;
            facilityToUpdate.Description = facility.Description;
            facilityToUpdate.Image = facility.Image;
            facilityToUpdate.Name = facility.Name;
            facilityToUpdate.SportCategories = facility.SportCategories;
            this.facilities.SaveChanges();
        }

        public void Add(Facility facility)
        {
            this.facilities.Add(facility);
            this.facilities.SaveChanges();
        }

        public void Save()
        {
            this.facilities.SaveChanges();
        }

        public void Remove(Facility facility)
        {
            this.facilities.Delete(facility.Id);
        }

        public void Dispose()
        {
            this.facilities.Dispose();
        }
    }
}
