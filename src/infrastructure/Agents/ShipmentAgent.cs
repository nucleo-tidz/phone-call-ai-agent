
using infrastructure.Plugins;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using model;
using System.Text.Json;

namespace infrastructure.Factory
{
    internal class ShipmentAgent(IShipmentAgentFactory agentFactory) : IAgent
    {
        public async Task<ContainerAgentResponse> Start(string userInput)
        {
            var agent = agentFactory.Create(string.Empty, string.Empty);

            var session = await agent.CreateSessionAsync();
          
            var response = await agent.RunAsync<ContainerAgentResponse>(userInput, session);
            return JsonSerializer.Deserialize<ContainerAgentResponse>(response.Text)!;
        }
    }
}
