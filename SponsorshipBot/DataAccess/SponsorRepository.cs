using System.Collections.Generic;
using Simple.Data;
using SponsorshipBot.Models;

namespace SponsorshipBot.DataAccess
{
    public class SponsorRepository
    {
        private readonly dynamic database = Database.Open();

        public IEnumerable<Sponsor> GetAllSponsors()
        {
            return database.Sponsors.All();
        }

        public void AddSponsor(Sponsor sponsor)
        {
            database.Sponsors.Insert(sponsor);
        }
    }
}