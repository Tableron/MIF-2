using System;
using System.Drawing;
using System.Windows.Forms;

namespace MIF2.UI
{
    partial class ParametersInputForm : Form
    {
        private MapForm _mapForm;

        public ParametersInputForm()
        {
            InitializeComponent();
        }

        private void ParametersInputForm_Load(object sender, EventArgs e)
        {
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public void SetMapForm(MapForm mapForm)
        {
            _mapForm = mapForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int countAgents, maxX, maxY;
                try
                {
                    countAgents = Int32.Parse(textBoxCountAgents.Text);
                }
                catch (Exception ex)
                {
                    textBoxCountAgents.ForeColor = Color.Red;
                    throw ex;
                }

                try
                {
                    maxX = Int32.Parse(textBoxMaxX.Text);
                }
                catch (Exception ex)
                {
                    textBoxMaxX.ForeColor = Color.Red;
                    throw ex;
                }

                try
                {
                    maxY = Int32.Parse(textBoxMaxY.Text);
                }
                catch (Exception ex)
                {
                    textBoxMaxY.ForeColor = Color.Red;
                    throw ex;
                }

                _mapForm.SetInitInfo(countAgents, maxX, maxY);
                
                Dispose();
            }
            catch (Exception)
            {
                
            }

        }
    }
}
