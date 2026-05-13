using MIF2.Models.Agents;
using System.Drawing;

namespace MIF2.Models.ColorAlgorithms
{
    class GreenWorld : ColorAlgorithm
    {
        public override Color CalculateColor(Agent agent)
        {
            return Color.Green;
        }

        public override bool NeedUpdateColor(int cycleCounter)
        {
            return false;
        }
    }
}
