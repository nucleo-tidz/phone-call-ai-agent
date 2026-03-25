using System.ComponentModel.DataAnnotations;

namespace infrastructure.Options
{
    public class AzureOpenAIOptions
    {
        public const string SectionName = "AzureOpenAI";

        [Required]
        public string Endpoint { get; set; } = string.Empty;
        [Required]
        public string ApiKey { get; set; } = string.Empty;
        [Required]
        public string ChatDeployment { get; set; } = string.Empty;
        [Required]
        public string EmbeddingDeployment { get; set; } = string.Empty;
    }
}
