using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using SponsorshipBot.Models;

namespace SponsorshipBot.DataAccess
{
    public class TotalsRepository
    {
        private readonly dynamic database = Database.Open();

        public Totals GetTotals(int conferenceId)
        {
            return GetAllTotals().Single(c => c.ConferenceId == conferenceId);
        }

        private IEnumerable<Totals> GetAllTotals()
        {
            return database.Totals.All();
        }

        public void UpdateTotalNeeded(int conferenceId, decimal totalNeeded)
        {
            var totals = GetTotals(conferenceId);
            totals.AmountNeeded = totalNeeded;
            database.Totals.Update(totals);
        }

        public void UpdateStartingBalance(int conferenceId, decimal startingBalance)
        {
            var totals = GetTotals(conferenceId);
            totals.StartingBalance = startingBalance;
            database.Totals.Update(totals);
        }
    }
}
