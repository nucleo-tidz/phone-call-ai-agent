using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Factory
{
    public interface IAgent
    {
        Task<ContainerAgentResponse> Start(string userInput);
    }
}
