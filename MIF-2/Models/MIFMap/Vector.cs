using MIF2.Models;
using System.Collections.Generic;

namespace MIF2.Models.MIFMap
{
    internal struct Vector
    {
        private static readonly Dictionary<byte, Vector> _values;

        public int DeltaX { get; private set; }
        public int DeltaY { get; private set; }

        public Vector(int deltaX, int deltaY)
        {
            DeltaX = deltaX; 
            DeltaY = deltaY;   
        }

        public static Vector RandomVector()
        {
            Randomizer rnd = new Randomizer();
            int deltaX = 0;
            int deltaY = 0;

            while (deltaX == 0 && deltaY == 0)
            {
                deltaX = rnd.Next(3) - 1;
                deltaY = rnd.Next(3) - 1;
            }

            return new Vector(deltaX, deltaY);
        }
    }
}
