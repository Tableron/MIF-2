namespace MIF2.Models.MIFMap
{
    internal class Cell
    {
        public IMIFObject MIFObject { get; set; }
        public MIFEnvironment MIFEnvironment { get; set; }

        public Cell()
        {
            MIFObject = null;
            MIFEnvironment = null;
        }

        public void RemoveMIFObject()
        {
            MIFObject = null;
        }
    }
}
