using MIF2.Models.MIFMap;
using System.Drawing;

namespace MIF2.Models.CycleInstructions
{
    class ObjectMove : ICycleInstruction
    {
        public Coordinates From { get; private set; }
        public Coordinates To { get; private set; }
        public Color ObjectColor { get; private set; }

        public ObjectMove(Coordinates from, Coordinates to, Color objectColor)
        {
            From = from;
            To = to;
            ObjectColor = objectColor;
        }
    }
}
