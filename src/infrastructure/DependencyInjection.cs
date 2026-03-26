
using infrastructure.Factory;
using infrastructure.Options;
using infrastructure.Plugins;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection Add(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddAI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<AzureOpenAIOptions>()
                .BindConfiguration(AzureOpenAIOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var options = configuration.GetSection(AzureOpenAIOptions.SectionName)
                .Get<AzureOpenAIOptions>()!;

            var client =
                new Azure.AI.OpenAI.AzureOpenAIClient(
                new Uri(options.Endpoint),
                new System.ClientModel.ApiKeyCredential(options.ApiKey));

            services.AddChatClient
                (client.GetChatClient(options.ChatDeployment)
                .AsIChatClient());

            services.AddEmbeddingGenerator<string, Embedding<float>>(client.GetEmbeddingClient(options.EmbeddingDeployment).AsIEmbeddingGenerator());

            return services;
        }
        public static IServiceCollection AddAgents(this IServiceCollection services)
        {
            services.AddSingleton<IShipmentAgentFactory,ShipmentAgentFactory>()
                .AddTransient<IAgent,ShipmentAgent>()
                .AddTransient<ShipmentPlugin>();
            return services;
        }
    }
}
