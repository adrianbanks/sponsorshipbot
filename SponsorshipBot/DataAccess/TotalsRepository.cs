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

        public void UpdateTotalNeeded(decimal totalNeeded)
        {
            var totals = GetTotals();
            totals.AmountNeeded = totalNeeded;
            database.Totals.Update(totals);
        }

        public void UpdateStartingBalance(decimal startingBalance)
        {
            var totals = GetTotals();
            totals.StartingBalance = startingBalance;
            database.Totals.Update(totals);
        }
    }
}
