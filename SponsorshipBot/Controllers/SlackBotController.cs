using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SponsorshipBot.Controllers
{
    public class SlackMessage
    {
        public string token { get; set; }
        public string team_id { get; set; }
        public string team_domain { get; set; }
        public string channel_id { get; set; }
        public string channel_name { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string command { get; set; }
        public string text { get; set; }
    }

    public class SlackBotController : ApiController
    {
        public HttpResponseMessage Endpoint(SlackMessage message)
        {
            var slackTeamToken = ConfigurationManager.AppSettings["Slack_team_token"];

            if (message.token != slackTeamToken)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid team token");
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

            return new HttpResponseMessage {Content = new StringContent("it worked!")};
        }
    }
}
