
using infrastructure.Plugins;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using model;
using System.Text.Json;

namespace infrastructure.Factory
{
    internal class ShipmentAgent(IChatClient chatClient, IServiceProvider serviceProvider) : IAgent
    {
        public AIAgent Create()
        {
            var plugin = serviceProvider.GetService<ShipmentPlugin>();

            return chatClient.AsAIAgent(new ChatClientAgentOptions
            {
                ChatOptions = new ChatOptions()
                {
                    ResponseFormat = Microsoft.Extensions.AI.ChatResponseFormat.ForJsonSchema<ContainerAgentResponse>(),
                    Instructions = """
                     You are a professional shipment assistant with expertise in freight and container logistics.
                     You help users retrieve accurate, up-to-date information about their shipment bookings.
                     
                     ## Capabilities
                     You can answer questions about a booking by looking up:
                     - Booking status (e.g. In Transit, Arrived, Pending)
                     - Total number of containers and their individual container numbers
                     - Total cargo weight in kilograms
                     - Port of origin and destination port
                     - Assigned vessel name and voyage number
                     - Estimated time of arrival (ETA)
                     
                     ## Behaviour
                     - Always ask for a booking ID before calling any tool, unless the user has already provided one.
                     - Call only the tools required to answer the user's specific question — do not retrieve unnecessary data.
                     - If a tool returns no data or an error, inform the user clearly and suggest they verify their booking ID.
                     - Never guess or fabricate shipment data. Only present information returned by your tools.
                     - If the user asks something outside your capabilities (e.g. modifying a booking), politely explain that you can only retrieve information and direct them to the appropriate team.
                     
                     ## Response style
                     - Be concise and professional.
                     - Present structured data (e.g. container lists) in a readable format.
                     - Always confirm the booking ID you used when presenting results.
                     """,
                    ToolMode = ChatToolMode.Auto,
                    Tools =
                    [
                        AIFunctionFactory.Create(plugin.GetTotalContainers),
                        AIFunctionFactory.Create(plugin.GetBookingStatus),
                        AIFunctionFactory.Create(plugin.GetTotalCargoWeight),
                        AIFunctionFactory.Create(plugin.GetOriginPort),
                        AIFunctionFactory.Create(plugin.GetDestinationPort),
                        AIFunctionFactory.Create(plugin.GetEstimatedArrival),
                        AIFunctionFactory.Create(plugin.GetVesselDetails),
                        AIFunctionFactory.Create(plugin.GetContainerNumbers),
                    ],
                },
                Description = "A shipment agent",
                ChatHistoryProvider = new InMemoryChatHistoryProvider(
                    new InMemoryChatHistoryProviderOptions()
                    {
                        ChatReducer = new MessageCountingChatReducer(10)
                    }),
            }, services: serviceProvider);
        }
        public async Task<ContainerAgentResponse> Start(string userInput)
        {
            var agent = Create();
            var session = await agent.CreateSessionAsync();
            var response = await agent.RunAsync(userInput, session);
            return JsonSerializer.Deserialize<ContainerAgentResponse>(response.Text)!;
        }
    }
}
