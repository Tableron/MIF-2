using MIF2.Models.Agents;
using MIF2.Models;

namespace MIF2.Models.GenomeReaders
{
    abstract class GenomeHandler
    {
        protected SimulationCycle _simulationCycle;

        protected int[] _deltaX = { -1, 0, 1 };
        protected int[] _deltaY = { -1, 0, 1 };

        public GenomeHandler Successor { get; set; } = null;

        public abstract bool HandleRequest(Agent agent, InstructionCode code);
    }
}