using MIF2.Models.ColorAlgorithms;
using MIF2.Controllers;
using System;

namespace MIF2.UI.UICommands
{
    class ColorAlgorithmCommand : UICommand
    {
        public ColorAlgorithms ColorAlgorithm { get; set; }

        public ColorAlgorithmCommand(Controller controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.SetColorAlgorithm(ColorAlgorithm);
        }

        public override void Undo()
        {
            _controller.SetColorAlgorithm(ColorAlgorithms.GreenWorld);
        }
    }
}
