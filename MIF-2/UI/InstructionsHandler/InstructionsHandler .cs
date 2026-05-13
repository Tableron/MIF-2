using MIF2.Models.CycleInstructions;

namespace MIF2.UI.InstructionsHandler
{
    class InstructionHandler
    {    
        private Handler _startHandler;

        public InstructionHandler(MapForm mapForm)
        {
            _startHandler = new HandlerObjectMove(mapForm);
            Handler h2 = new HandlerUIUpdate(mapForm);

            _startHandler.Successor = h2;
        }

        public void HandleRequest(ICycleInstruction instruction)
        {
            _startHandler.HandleRequest(instruction);
        }
    }


}
