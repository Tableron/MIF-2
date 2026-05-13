using MIF2.Controllers;

namespace MIF2.UI.UICommands
{
    abstract class UICommand
    {
        protected Controller _controller;
        public abstract void Execute();
        public abstract void Undo();
    }
}
