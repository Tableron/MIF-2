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
            textBoxCountAgents.ForeColor = Color.Black;
            textBoxMaxX.ForeColor = Color.Black;
            textBoxMaxY.ForeColor = Color.Black;

            bool hasErrors = false;
            int countAgents = 0, maxX = 0, maxY = 0;

            if (!Int32.TryParse(textBoxCountAgents.Text, out countAgents) || countAgents <= 0)
            {
                textBoxCountAgents.ForeColor = Color.Red;
                hasErrors = true;
            }

            if (!Int32.TryParse(textBoxMaxX.Text, out maxX) || maxX <= 0)
            {
                textBoxMaxX.ForeColor = Color.Red;
                hasErrors = true;
            }

            if (!Int32.TryParse(textBoxMaxY.Text, out maxY) || maxY <= 0)
            {
                textBoxMaxY.ForeColor = Color.Red;
                hasErrors = true;
            }

            if (hasErrors)
            {
                MessageBox.Show(
                    "Параметры должны быть положительными целыми числами.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (countAgents > maxX * maxY)
            {
                MessageBox.Show(
                    $"Агентов ({countAgents}) больше, чем клеток в мире ({maxX}×{maxY}={maxX * maxY}).",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _mapForm.SetInitInfo(countAgents, maxX, maxY);
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось инициализировать симуляцию:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
