namespace SportsBook.Services.Data.Contracts
{
    using System.Linq;
    using SportsBook.Data.Models;

    public interface IFacilitiesService
    {
        IQueryable<Facility> GetTopFacilities();

        IQueryable<Facility> All(int page = 1, int pageSize = 10);

        Facility GetFacilityDetails(int id);

        IQueryable<FacilityComment> GetLatestFacilityComments(int id);

        void UpdateFacility(int id, Facility facility);

        void Add(Facility facility);

        void Remove(Facility facility);

        void Save();

        void Dispose();
    }
}
