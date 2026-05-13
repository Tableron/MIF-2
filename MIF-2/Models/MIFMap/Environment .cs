namespace MIF2.Models.MIFMap
{
    internal class MIFEnvironment
    {
        public float Illumination { get; set; }
        public float Mutagenicity { get; set; }
        public float Density { get; set; }
        public float Temperature { get; set; }

        public MIFEnvironment()
        {
            Illumination = 1;
            Mutagenicity = 0;
            Density = 0;
            Temperature = 1;
        }
    }
}
