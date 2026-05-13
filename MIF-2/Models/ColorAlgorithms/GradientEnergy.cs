using MIF2.Models.Agents;
using System.Drawing;


namespace MIF2.Models.ColorAlgorithms
{
    class GradientEnergy : ColorAlgorithm
    {
        public override Color CalculateColor(Agent agent)
        {
            int maxEnergy = 500;
            float energy = agent.Energy;
            float energyRatio = energy / maxEnergy;
            float rg = energyRatio > 1 ? 255 : energyRatio * 255f;
            if (rg < 0) { rg = 0; }

            return Color.FromArgb((int)rg, (int)rg, 0);
        }

        public override bool NeedUpdateColor(int cycleCounter)
        {
            return true;
            if(cycleCounter % 5 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
