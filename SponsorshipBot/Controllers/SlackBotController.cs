using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SponsorshipBot.Commands;
using SponsorshipBot.Models;

namespace SponsorshipBot.Controllers
{
    public class SlackBotController : ApiController
    {
        public HttpResponseMessage Endpoint(SlackMessage message)
        {
            try
            {
                var slackTeamToken = ConfigurationManager.AppSettings["Slack_team_token"];

                if (message.token != slackTeamToken)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid team token");
                }

                var commandParser = new CommandParser();
                var command = commandParser.ParseCommand(message);
                var response = command.Execute();
                return new HttpResponseMessage { Content = new StringContent(response) };
            }
            catch (Exception exception)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message, exception);
            }
        }
    }
}
