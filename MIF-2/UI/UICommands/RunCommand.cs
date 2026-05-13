using MIF2.Controllers;

namespace MIF2.UI.UICommands
{
    class RunCommand : UICommand
    {
        public RunCommand(Controller controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.Start();
        }

        public override void Undo()
        {
            _controller.Stop();
        }
    }
}
