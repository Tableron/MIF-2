using MIF2.Models.Agents;
using System.Drawing;

namespace MIF2.Models.ColorAlgorithms
{
    public enum ColorAlgorithms
    {
        GreenWorld,
        EnergyGradient,
        IntegrityGradient,
    }

    abstract class ColorAlgorithm
    {
        public abstract Color CalculateColor(Agent agent);

        public abstract bool NeedUpdateColor(int cycleCounter);
    }
}
