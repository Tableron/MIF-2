using MIF2.Models.Agents;
using MIF2.Models.GenomeReaders;
using MIF2.Models;
using MIF2.Models.MIFMap;

namespace MIF2.Models.GenomeReaders
{
    class HandlerMitosis : GenomeHandler
    {
        private VectorConverter _vectorConverter;

        public HandlerMitosis(SimulationCycle cycle)
        {
            _simulationCycle = cycle;
            _vectorConverter = new VectorConverter();
        }

        public override bool HandleRequest(Agent agent, InstructionCode code)
        {
            if (code == InstructionCode.Mitosis)
            {
                // Определение целевой клетки
                Vector vector = _vectorConverter.ParseVectorCode(agent.NexGen());

                Agent newAgent = agent.Mitosis(vector);
                if (newAgent != null)
                {
                    _simulationCycle.Agents.Add(newAgent);
                }

                return true;
            }
            else if (Successor != null)
            {
                return Successor.HandleRequest(agent, code);
            }

            return false;
        }

    }
}
