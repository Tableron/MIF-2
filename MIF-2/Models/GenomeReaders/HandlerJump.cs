using MIF2.Models.Agents;

namespace MIF2.Models.GenomeReaders
{
    class HandlerJump : GenomeHandler
    {
        public HandlerJump(SimulationCycle cycle)
        {
            _simulationCycle = cycle;
        }

        public override bool HandleRequest(Agent agent, InstructionCode code)
        {
            if (code == InstructionCode.Attack)
            {
                // Определение целевой клетки
                byte delta = agent.NexGen();

                agent.Jump(delta);


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
