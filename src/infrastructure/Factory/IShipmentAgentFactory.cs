using Microsoft.Agents.AI;

namespace infrastructure.Factory
{
    internal interface IShipmentAgentFactory
    {
        AIAgent Create(string userId, string conversationId);
    }
}
