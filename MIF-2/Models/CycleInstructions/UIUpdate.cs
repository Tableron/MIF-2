

namespace MIF2.Models.CycleInstructions
{
    class UIUpdate : ICycleInstruction
    {
        public int Cycle { get; private set; }
        public int AgentsCount { get; private set; }
        public bool DoScreen { get; private set; }

        public UIUpdate(int day, int agentsCount, bool doScreen)
        {
            Cycle = day;
            AgentsCount = agentsCount;
            DoScreen = doScreen;
        }

        public UIUpdate(int day, int agentsCount) : this(day, agentsCount, false)
        {

        }
    }
}
