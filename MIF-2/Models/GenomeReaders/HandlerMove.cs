using MIF2.Models.Agents;
using MIF2.Models.MIFMap;

namespace MIF2.Models.GenomeReaders
{
    class HandlerMove : GenomeHandler
    {
        private VectorConverter _vectorConverter;

        public HandlerMove(SimulationCycle cycle)
        {
            _simulationCycle = cycle;
            _vectorConverter = new VectorConverter();
        }

        public override bool HandleRequest(Agent agent, InstructionCode code)
        {
            if (code == InstructionCode.Move)
            {
                // Определение целевой ячейки
                Vector vector = _vectorConverter.ParseVectorCode(agent.NexGen());

                // Перемещение организма
                _simulationCycle.SetOldCoordinates(agent.Coordinates);
                agent.Move(vector);
                _simulationCycle.SetNewCoordinates(agent.Coordinates);

                // Отправка команды отрисовки
                _simulationCycle.SendMoveInstruction(agent);

                // Вычесть энергию за перемещение
                agent.SpendEnergy(Constraints.BaseMoveEnergy + agent.GetEnvironment().Density);

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
