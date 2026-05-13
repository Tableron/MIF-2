using MIF2.Models.Agents;
using System.Drawing;

namespace MIF2.Models.ColorAlgorithms
{
    class GradientIntegrity : ColorAlgorithm
    {
        public override Color CalculateColor(Agent agent)
        {
            int maxIntegrity = 100;
            float integrity = agent.Integrity;
            float integrityRatio = integrity / maxIntegrity;
            float b = integrityRatio > 1 ? 255 : integrityRatio * 255f;
            float r = 255 - b;
            return Color.FromArgb((int)r, 0, (int)b);
        }

        public override bool NeedUpdateColor(int cycleCounter)
        {
            if (cycleCounter % 5 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
