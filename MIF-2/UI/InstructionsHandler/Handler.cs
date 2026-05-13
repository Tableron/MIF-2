using MIF2.Models.CycleInstructions;

namespace MIF2.UI.InstructionsHandler
{
    abstract class Handler
    {
        protected MapForm _mapForm;
        public Handler Successor { get; set; } = null;
        public abstract void HandleRequest(ICycleInstruction instruction);
    }
}
