using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIF2.Models
{
    static class Constraints
    {
        public static int MaxSizeQueueInstruction { get; private set; } = 100;

        public static int DefaultGenomeSize { get; private set; } = 10;
        public static int MaxGenomeSize { get; private set; } = 256;
        public static int MaxAgentAge { get; private set; } = 100;
        public static int DefaultAgentIntegrity { get; private set; } = 100;
        public static int DefaultAgentEnergy { get; private set; } = 50;
        public static int MaxNumberAttemptsAgentExecuteGenome { get; private set; } = 5;

        public static int BasePhotosynthesisEnergy { get; private set; } = 10;

        public static int BaseEverydayEnergy { get; private set; } = 1;
        public static int BaseAttackEnergy { get; private set; } = 2;
        public static int BaseMitosisEnergy { get; private set; } = 20;
        public static int BaseMoveEnergy { get; private set; } = 2;

        public static int SaveEveryNTicks { get; private set; } = 100;

        public static int BaseDamage { get; private set; } = 20;


    }
}
