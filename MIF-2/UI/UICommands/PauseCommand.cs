using MIF2.Controllers;

namespace MIF2.UI.UICommands
{
    class PauseCommand : UICommand
    {
        public PauseCommand(Controller controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.Pause();
        }

        public override void Undo()
        {
            _controller.Continue();
        }
    }
}
