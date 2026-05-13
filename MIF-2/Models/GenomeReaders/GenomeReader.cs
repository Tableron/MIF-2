using MIF2.Models.Agents;

namespace MIF2.Models.GenomeReaders
{
    class GenomeReader
    {    
        private GenomeHandler _startHandler;

        public GenomeReader(SimulationCycle cycle)
        {
            _startHandler = new HandlerPhotosynthesis(cycle);
            GenomeHandler h2 = new HandlerMove(cycle);
            GenomeHandler h3 = new HandlerAttack(cycle);
            GenomeHandler h4 = new HandlerMitosis(cycle);
            GenomeHandler h5 = new HandlerJump(cycle);
            //GenomeHandler h6 = new HandlerRegeneration(cycle);
            //GenomeHandler h7 = new HandlerSelfDestruction(cycle);
            //GenomeHandler h8 = new HandlerProlong(cycle);

            _startHandler.Successor = h2;
            h2.Successor = h3;
            h3.Successor = h4;
            h4.Successor = h5;
            //h5.Successor = h6;
            //h4.Successor = h7;
            //h7.Successor = h8;
        }

        public void ExecuteGenome(Agent agent)
        {
            InstructionCode code = (InstructionCode)agent.NexGen();
            if (code >= InstructionCode.NoInstruction)
            {
                return;
            }

            int countAttempts = 0; 
            while (countAttempts < Constraints.MaxNumberAttemptsAgentExecuteGenome)
            {
                countAttempts++;
                if(_startHandler.HandleRequest(agent, code) == false)
                {
                    code = (InstructionCode)agent.NexGen();
                    if (code >= InstructionCode.NoInstruction)
                        return;
                }
                else
                {
                    break;
                }

            }
        }
    }
}
