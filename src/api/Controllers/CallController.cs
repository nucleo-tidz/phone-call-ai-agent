using infrastructure.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateRequest]
    public class CallController(IConfiguration configuration, IAgent agent) : TwilioController
    {
        [HttpPost]
        public async Task<TwiMLResult> IncomingCall()
        {
            var agentResponse = await agent.Start("hello");
            var baseUrl = configuration["App:BaseUrl"];
            var response = new VoiceResponse();
            var gather = new Gather(input:
                             new List<Gather.InputEnum> { Gather.InputEnum.Speech },
                                    action: new Uri($"{baseUrl}/api/call/process"),
                                    method: Twilio.Http.HttpMethod.Post
                                    );
            gather.Say(agentResponse.Response);
            response.Append(gather);

            return TwiML(response);
        }
        [HttpPost("process")]
        public async Task<TwiMLResult> Process([FromForm] string speechResult)
        {
           
            var baseUrl = configuration["App:BaseUrl"];
            var response = new VoiceResponse();

            if (!string.IsNullOrWhiteSpace(speechResult))
            {
                var agentResponse = await agent.Start(speechResult);
                response.Say(agentResponse.Response);
            }

            var gather = new Gather(input: new List<Gather.InputEnum> { Gather.InputEnum.Speech },
                                    action: new Uri($"{baseUrl}/api/call/process"),
                                    method: Twilio.Http.HttpMethod.Post);
            response.Append(gather);

            return TwiML(response);
        }
    }
}
