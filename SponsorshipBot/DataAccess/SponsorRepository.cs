﻿using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using SponsorshipBot.Models;

namespace SponsorshipBot.DataAccess
{
    public class SponsorRepository
    {
        private readonly dynamic database = Database.Open();

        public IEnumerable<Sponsor> GetAllSponsors(int conferenceId)
        {
            return GetAllSponsors().Where(s => s.ConferenceId == conferenceId);
        }

        private IEnumerable<Sponsor> GetAllSponsors()
        {
            return database.Sponsors.All();
        }

        public Sponsor Get(string sponsorName)
        {
            return database.Sponsors.Find(database.Sponsors.Name == sponsorName);
        }

        public void AddSponsor(Sponsor sponsor)
        {
            database.Sponsors.Insert(sponsor);
        }

        public void UpdateSponsor(Sponsor sponsor)
        {
            database.Sponsors.UpdateById(sponsor);
        }

        public void DeleteSponsor(string name)
        {
            database.Sponsors.Delete(Name: name);
        }
    }
}
