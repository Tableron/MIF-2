enum InstructionCode : byte
{
    Photosynthesis = 0,
    Move = 1, 
    Attack = 2,
    Mitosis = 3,
    Jump = 4,
    NoInstruction = 5 // Оставлять последней в enum
}

namespace MIF2.Models.Agents
{
    class Genome
    {
        public byte[] _genome;
        private int _genomeSize;
        private int _position;

        public Genome()
        {
            _position = 0;
        }

        public byte NextGen()
        {
            byte gen = _genome[_position];

            _position++;
            if (_position == _genomeSize)
                _position = 0;

            return gen;
        }

        public Genome Clone()
        {
            Genome copy = new Genome
            {
                _genomeSize = _genomeSize,
                _genome = new byte[_genomeSize]
            };

            for (int i = 0; i < _genomeSize; i++)
            {
                copy._genome[i] = _genome[i];
            }

            return copy;
        }

        public void Mutate(float mutagenicity)
        {
            Randomizer rnd = new Randomizer();
            int random = rnd.Next();

            _genome[rnd.Next(_genomeSize)] = (byte)rnd.Next();

            if (random > int.MaxValue * (0.6 - mutagenicity))
            {
                _genome[rnd.Next(_genomeSize)] = (byte)rnd.Next();
            }

            if (random > int.MaxValue * (0.7 - mutagenicity))
            {
                _genome[rnd.Next(_genomeSize)] = (byte)rnd.Next();
            }

            if (random > int.MaxValue * (0.8 - mutagenicity))
            {
                _genome[rnd.Next(_genomeSize)] = (byte)rnd.Next();
            }

            if (random > int.MaxValue * (0.9 - mutagenicity) && random % 2 == 0)
            {
                byte[] newGenome = new byte[_genomeSize + 1];
                for (int i = 0; i < _genomeSize; i++)
                {
                    newGenome[i] = _genome[i];
                }
                newGenome[_genomeSize] = (byte)rnd.Next();

                _genomeSize++;
                _genome = newGenome;
            }

            if (random > int.MaxValue * (0.9 - mutagenicity) && random % 2 == 1)
            {
                byte[] newGenome = new byte[_genomeSize - 1];
                for (int i = 0; i < _genomeSize - 1; i++)
                {
                    newGenome[i] = _genome[i];
                }

                _genomeSize--;
                _genome = newGenome;
            }
        }

        public void SetDefaultGenome()
        {
            _genomeSize = Constraints.DefaultGenomeSize;
            _genome = new byte[_genomeSize];

            for (int i = 0; i < _genomeSize; i++)
            {
                _genome[i] = (byte)InstructionCode.Photosynthesis;
            }
            _genome[0] = (byte)InstructionCode.Move;
            _genome[1] = 3;
            _genome[2] = (byte)InstructionCode.Mitosis;
            _genome[3] = 1;
        }

        public override string ToString()
        {
            string str = $"{_genome.Length, -5} | ";
            foreach (byte gen in _genome)
            {
                str += $"{gen, -4}";
            }
            return str;
        }

        public void Jump(byte delta)
        {
            _position = (_position + delta) % _genomeSize;
        }
    }
}
