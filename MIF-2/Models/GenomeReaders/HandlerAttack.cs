using MIF2.Models.Agents;
using MIF2.Models.MIFMap;

namespace MIF2.Models.GenomeReaders
{
    class HandlerAttack : GenomeHandler
    {
        private VectorConverter _vectorConverter;

        public HandlerAttack(SimulationCycle cycle)
        {
            _simulationCycle = cycle;
            _vectorConverter = new VectorConverter();
        }

        public override bool HandleRequest(Agent agent, InstructionCode code)
        {
            if (code == InstructionCode.Attack)
            {
                // Определение целевой клетки
                Vector vector = _vectorConverter.ParseVectorCode(agent.NexGen());

                agent.Attack(vector);

                // Вычесть энергию за атаку
                agent.SpendEnergy(Constraints.BaseAttackEnergy);

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
