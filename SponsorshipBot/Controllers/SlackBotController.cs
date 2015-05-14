using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using SponsorshipBot.DataAccess;
using SponsorshipBot.Models;

namespace SponsorshipBot.Controllers
{
    public class SlackBotController : ApiController
    {
        private readonly SponsorRepository sponsorRepository = new SponsorRepository();

        public HttpResponseMessage Endpoint(SlackMessage message)
        {
            try
            {
                var slackTeamToken = ConfigurationManager.AppSettings["Slack_team_token"];

                if (message.token != slackTeamToken)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid team token");
                }

                var response = EndpointImpl(message);
                return new HttpResponseMessage { Content = new StringContent(response) };
            }
            catch (Exception exception)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message, exception);
            }
        }

        private string EndpointImpl(SlackMessage message)
        {
            var allSponsors = sponsorRepository.GetAllSponsors();
            var text = new StringBuilder();

            foreach (var sponsor in allSponsors)
            {
                text.AppendLine(sponsor.Name);
            }

            /*
            token=mOecPFOndUks8NQCPgX6ZSn5
            team_id=T0001
            team_domain=example
            channel_id=C2147483705
            channel_name=test
            user_id=U2147483697
            user_name=Steve
            command=/weather
            text=94070
            */

            return text.ToString();
        }
    }
}
