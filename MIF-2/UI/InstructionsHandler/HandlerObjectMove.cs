using MIF2.Models.CycleInstructions;
using MIF2.Models.Agents;
using MIF2.Models.MIFMap;

namespace MIF2.UI.InstructionsHandler
{
    class HandlerObjectMove : Handler
    {
        private int _borderCell;
        private int _borderHalfCell;

        public HandlerObjectMove(MapForm mapForm)
        {
            _mapForm = mapForm;
            _borderCell = _mapForm.BorderWidth + _mapForm.CellSize;
            _borderHalfCell = _mapForm.BorderWidth + _mapForm.CellSize / 2;
        }

        public override void HandleRequest(ICycleInstruction instruction)
        {
            if (instruction is ObjectMove)
            {
                ObjectMove moveInstruction = instruction as ObjectMove;

                //Закрашивание текущей позиции
                int x, y;
                Coordinates from = moveInstruction.From;
                if(from != null)
                {
                    x = _borderCell * from.X + _mapForm.BorderWidth;
                    y = _borderCell * from.Y + _mapForm.BorderWidth;
                    _mapForm.DrawCell(x, y, _mapForm.BaseColor);
                }

                //Закрашивание новой позиции
                Coordinates to = (instruction as ObjectMove).To;
                if(to != null)
                {
                    x = _borderCell * to.X + _mapForm.BorderWidth;
                    y = _borderCell * to.Y + _mapForm.BorderWidth;
                    _mapForm.DrawCell(x, y, moveInstruction.ObjectColor);
                }

            }
            else
            {
                Successor?.HandleRequest(instruction);
            }
        }
    }
}
