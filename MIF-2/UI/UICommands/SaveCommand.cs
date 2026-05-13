using System;
using MIF2.Controllers;

namespace MIF2.UI.UICommands
{
    class SaveCommand : UICommand
    {
        public SaveCommand(Controller controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.Save();
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
