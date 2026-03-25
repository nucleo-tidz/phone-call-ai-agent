using System.Text.Json.Serialization;

namespace model
{
    public class ContainerAgentResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; } = string.Empty;

    }
}
