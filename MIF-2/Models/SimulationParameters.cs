using System;
using System.IO;
using System.Text.Json;

namespace MIF2.Models
{
    public class SimulationParameters
    {
        public DateTime StartedAt { get; set; }
        public int RandomSeed { get; set; }

        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int InitialAgentsCount { get; set; }

        public float Illumination { get; set; }
        public float Mutagenicity { get; set; }
        public float Density { get; set; }
        public float Temperature { get; set; }

        public int DefaultGenomeSize { get; set; }
        public int MaxGenomeSize { get; set; }
        public int MaxAgentAge { get; set; }
        public int DefaultAgentIntegrity { get; set; }
        public int DefaultAgentEnergy { get; set; }

        public int BasePhotosynthesisEnergy { get; set; }
        public int BaseEverydayEnergy { get; set; }
        public int BaseAttackEnergy { get; set; }
        public int BaseMitosisEnergy { get; set; }
        public int BaseMoveEnergy { get; set; }
        public int BaseDamage { get; set; }

        public int SaveEveryNTicks { get; set; }

        public void SaveToFile(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(path, json);
        }
    }
}