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
    public class CallController(IConfiguration configuration) : TwilioController
    {
        [HttpPost]
        public TwiMLResult IncomingCall()
        {
            var baseUrl = configuration["App:BaseUrl"];
            var response = new VoiceResponse();
            var gather = new Gather(input: 
                             new List<Gather.InputEnum> { Gather.InputEnum.Speech },
                                    action: new Uri($"{baseUrl}/call/process"),
                                    method: Twilio.Http.HttpMethod.Post
                                    );
            gather.Say("Welcome to nucleus shipment , How can I help ?");
            response.Append(gather);

            return TwiML(response);
        }
        [HttpPost("process")]
        public TwiMLResult Process([FromForm] string speechResult)
        {

            var baseUrl = configuration["App:BaseUrl"];
            var response = new VoiceResponse();

            if (!string.IsNullOrWhiteSpace(speechResult))
            {
                response.Say($"You said {speechResult}");
            }

            var gather = new Gather(input: new List<Gather.InputEnum> { Gather.InputEnum.Speech },
                                    action: new Uri($"{baseUrl}/agent/process"),
                                    method: Twilio.Http.HttpMethod.Post);
            response.Append(gather);

            return TwiML(response);
        }
    }
}
