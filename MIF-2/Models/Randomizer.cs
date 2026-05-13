using System;

namespace MIF2.Models
{
    public class Randomizer
    {
        private static Random _random;
        private static readonly object _lock = new object();
        private static int _seed;

        public static int Seed => _seed;

        public static void Initialize(int seed)
        {
            lock (_lock)
            {
                _seed = seed;
                _random = new Random(seed);
            }
        }

        public Randomizer()
        {
            if (_random == null)
            {
                throw new InvalidOperationException(
                    "Randomizer.Initialize(seed) must be called before any Randomizer is used.");
            }
        }

        public int Next()
        {
            lock (_lock)
            {
                return _random.Next();
            }
        }

        public int Next(int max)
        {
            lock (_lock)
            {
                return _random.Next(max);
            }
        }
    }
}
