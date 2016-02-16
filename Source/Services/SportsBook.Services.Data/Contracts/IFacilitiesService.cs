namespace SportsBook.Services.Data.Contracts
{
    using System.Linq;
    using SportsBook.Data.Models;

    public interface IFacilitiesService
    {
        IQueryable<Facility> GetTopFacilities();

        IQueryable<AppUser> All(int page = 1, int pageSize = 10);

        Facility GetFacilityDetails(int id);

        void UpdateFacility();

        void Add(Facility facility);
    }
}
