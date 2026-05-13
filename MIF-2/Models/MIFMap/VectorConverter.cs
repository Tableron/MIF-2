using System.Collections.Generic;

namespace MIF2.Models.MIFMap
{
    internal class VectorConverter
    {
        private static Dictionary<byte, Vector> _table = null;

        // |8|1|2|
        // |7|*|3|
        // |6|5|4|

        public VectorConverter()
        {
            if (_table is null)
            {
                _table = new Dictionary<byte, Vector>
                {
                    { 8, new Vector(-1, -1) },
                    { 1, new Vector(0, -1) },
                    { 2, new Vector(1, -1) },
                    { 7, new Vector(-1, 0) },
                    { 3, new Vector(1, 0) },
                    { 6, new Vector(-1, 1) },
                    { 5, new Vector(0, 1) },
                    { 4, new Vector(1, 1) }
                };
            }
        }

        public Vector ParseVectorCode(byte code)
        {
            if(code % _table.Count == 0)
            {
                return Vector.RandomVector();
            }
            else
            {
                return _table[(byte)(code % _table.Count)];
            }
        }
    }
}
