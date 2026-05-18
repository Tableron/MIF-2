using MIF2.Models.MIFMap;
using System;

namespace MIF2.Models.Agents
{
    class Agent : IMIFObject
    {
        private static long _count = 0;
        private long _id;
        private Map _map;

        public Genome _genome;
        public float Energy { get; private set; }
        public float Integrity { get; private set; }
        public int Age { get; set; }
        public Coordinates Coordinates { get; set; }
        public long Id => _id;

        public Agent(float energy)
        {
            _id = _count;
            _count++;
            Age = 0;
            Energy = energy;
        }

        public byte NexGen()
        {
            return _genome.NextGen();
        }

        public MIFEnvironment GetEnvironment()
        {
            Cell cell = _map.GetCell(Coordinates);
            return cell.MIFEnvironment;
        }

        public void AddMap(Map map)
        {
            _map = map;
        }

        public void DefaultAgent()
        {
            _genome = new Genome();
            _genome.SetDefaultGenome();
            Integrity = Constraints.DefaultAgentIntegrity;
        }

        public void NextCycle()
        {
            SpendEnergy(
                Constraints.BaseEverydayEnergy * GetEnvironment().Temperature);

            Age++;
        }

        public Agent Mitosis(Vector vector)
        {
            if (Energy < Constraints.DefaultAgentEnergy * 2)
            {
                return null;
            }

            Coordinates newCoordinates = _map.CalculateCoordinates(Coordinates, vector);
            if (_map.GetMIFObject(newCoordinates) is null)
            {
                Integrity = Constraints.DefaultAgentIntegrity;
                Age = 0;

                float newEnergy = (Energy - Constraints.BaseMitosisEnergy) / 2;
                Energy = newEnergy;

                Agent newAgent = new Agent(newEnergy)
                {
                    Integrity = Constraints.DefaultAgentIntegrity,
                    Age = 0,
                    Coordinates = newCoordinates,
                    _genome = _genome.Clone()
                };

                float mutagenicity = GetEnvironment().Mutagenicity;
                newAgent._genome.Mutate(mutagenicity);
                newAgent.AddMap(_map);

                _map.SetMIFObject(newAgent, newCoordinates);

                return newAgent;
            }
            return null;
        }

        public void Photosynthesis()
        {
            Energy += GetEnvironment().Illumination * Constraints.BasePhotosynthesisEnergy;
        }

        public void Move(Vector moveVector)
        {
            Coordinates = _map.Move(Coordinates, moveVector);
        }

        public void Attack(Vector attackVector)
        {
            IMIFObject obj = _map.GetMIFObject(Coordinates, attackVector);
            if (obj is Agent)
            {
                Agent target = obj as Agent;
                Energy += target.ApplyDamage(Constraints.BaseDamage);
            }
        }

        public float ApplyDamage(float damage)
        {
            Integrity -= damage;
            if (Integrity <= 0)
            {
                return Energy;
            }
            return 0;
        }

        public void SpendEnergy(float energy)
        {
            Energy -= energy;
        }

        public bool CanLive()
        {
            return 
                Energy > 0 && 
                Integrity > 0 && 
                Age < Constraints.MaxAgentAge;
        }

        internal void Jump(byte delta)
        {
            _genome.Jump(delta);
        }

        public static void ResetIdCounter()
        {
            _count = 0;
        }

        public override string ToString()
        {
            return _genome.ToString();
        }
    }
}
