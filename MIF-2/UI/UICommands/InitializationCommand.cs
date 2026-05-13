using MIF2.Controllers;
using System;

namespace MIF2.UI.UICommands
{
    class InitializationCommand : UICommand
    {
        public int CountAgents { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public InitializationCommand(Controller controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.InitialSimulation(CountAgents, MaxX, MaxY);
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
