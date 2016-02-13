namespace SportsBook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsBook.Data.Models;

    public interface ICommentsService
    {

        FacilityComment Add(string title, string content, string userId);
    }
}
