using MIF2.Models.Agents;
using MIF2.Models;

namespace MIF2.Models.GenomeReaders
{
    class HandlerPhotosynthesis : GenomeHandler
    {
        public HandlerPhotosynthesis(SimulationCycle cycle)
        {
            _simulationCycle = cycle;
        }

        public override bool HandleRequest(Agent agent, InstructionCode code)
        {
            if (code == InstructionCode.Photosynthesis)
            {
                agent.Photosynthesis();
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
