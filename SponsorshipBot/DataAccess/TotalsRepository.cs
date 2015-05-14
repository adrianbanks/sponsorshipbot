using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using SponsorshipBot.Models;

namespace SponsorshipBot.DataAccess
{
    public class TotalsRepository
    {
        private readonly dynamic database = Database.Open();

        public Totals GetTotals()
        {
            return GetAllTotals().Single();
        }

        private IEnumerable<Totals> GetAllTotals()
        {
            return database.Totals.All();
        }
    }
}
