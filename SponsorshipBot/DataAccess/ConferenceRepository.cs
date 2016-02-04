using System.Configuration;
using Simple.Data;
using SponsorshipBot.Models;

namespace SponsorshipBot.DataAccess
{
    public class ConferenceRepository
    {
        private readonly dynamic database = Database.Open();

        public Conference GetCurrentConference()
        {
            string conferenceName = ConfigurationManager.AppSettings["Conference_name"];
            return database.Conferences.Find(database.Conferences.Name == conferenceName);
        }
    }
}
