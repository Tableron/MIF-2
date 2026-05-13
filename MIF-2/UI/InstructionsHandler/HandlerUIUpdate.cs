using MIF2.Models.CycleInstructions;

namespace MIF2.UI.InstructionsHandler
{
    class HandlerUIUpdate : Handler
    {
        public HandlerUIUpdate(MapForm mapForm)
        {
            _mapForm = mapForm;
        }

        public override void HandleRequest(ICycleInstruction instruction)
        {
            if (instruction is UIUpdate uiUpdate)
            {
                _mapForm.UpdateMap(uiUpdate);
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(instruction);
            }
        }
    }
}
