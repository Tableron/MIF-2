using System;

namespace MIF2.Models
{
    class Randomizer
    {
        private static Random random = null;
        private static readonly object balanceLock = new object();

        private int _current;

        public Randomizer()
        {
            if (random == null)
            {
                random = new Random();
            }
        }


        public Randomizer(int seed)
        {
            if (random == null)
            {
                _current = seed;
                random = new Random(seed);
            }
        }

        public int Next()
        {
            lock (balanceLock)
            {
                _current = random.Next();
                return _current;
            }
        }

        public int Next(int mod)
        {
            lock (balanceLock)
            {
                _current = random.Next();
                return _current % mod;
            } 
        }
    }
}
