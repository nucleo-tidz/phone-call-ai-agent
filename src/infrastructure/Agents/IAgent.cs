using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.Factory
{
    internal interface IAgent
    {
        AIAgent Create();
    }
}
