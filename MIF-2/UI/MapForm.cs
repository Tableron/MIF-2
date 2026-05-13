using MIF2.Models.ColorAlgorithms;
using MIF2.Controllers;
using MIF2.Models.CycleInstructions;
using MIF2.UI.InstructionsHandler;
using MIF2.UI.UICommands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MIF2.UI
{
    partial class MapForm : Form
    {
        private PauseCommand _pauseCommand;
        private RunCommand _runCommand;
        private SaveCommand _saveCommand;
        private InitializationCommand _initializationCommand;
        private ColorAlgorithmCommand _colorAlgorithmCommand;

        private Thread _mapUpdate;

        private bool _pause = false;
        private bool _stop = false;
        private Stopwatch _stopwatch = new Stopwatch();

        private Queue<ICycleInstruction> _queueInstructionUI;
        private Controller _controller;

        // -------------------------------------

        private InstructionHandler _instructionHandler;

        public int CellSize { get; private set; } = 10;
        public int BorderWidth { get; private set; } = 1;
        public int XCountCells { get; set; }
        public int YCountCells { get; set; }

        private Color _borderColor = Color.White;

        public Color BaseColor;

        private Bitmap _bitmap;

        private bool _needDrawMap = true;
        private readonly object balanceLock = new object();

        public bool NeedDrawMap()
        {
            return _needDrawMap;
        }

        public MapForm()
        {
            InitializeComponent();
            _instructionHandler = new InstructionHandler(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonPause.Enabled = false;
            BaseColor = mapPanel.BackColor;
            radioButton1.Checked = true;
            radioButton8.Checked = true;

            // Элементы интерфейса для отладки
            bool isDebug = false;
            button1.Visible = isDebug;
            textBoxQueueSize.Visible = isDebug;
            richTextBoxStressTestOut.Visible = isDebug;
        }

        public void PrintSimulationStatus(string message, Status status)
        {
            switch (status)
            {
                case Status.Error:

                    break;

                default:
                    break;
            }
        }

        private void DrawMap()
        {
            if(_needDrawMap == false)
            {
                return;
            }

            mapPanel.Size = new Size(
                XCountCells * (BorderWidth + CellSize) + BorderWidth, 
                YCountCells * (BorderWidth + CellSize) + BorderWidth);

            _bitmap = new Bitmap(mapPanel.Width, mapPanel.Height);

            for (int y = 0; y < YCountCells + 1; y++)
            {
                for (int x = 0; x < mapPanel.Width; x++)
                {
                    for (int i = 0; i < BorderWidth; i++)
                    {
                        _bitmap.SetPixel(x, y * (BorderWidth + CellSize) + i, _borderColor);
                    }

                }
            }

            for (int x = 0; x < XCountCells + 1; x++)
            {
                for (int y = 0; y < mapPanel.Height; y++)
                {
                    for (int i = 0; i < BorderWidth; i++)
                    {
                        _bitmap.SetPixel(x * (BorderWidth + CellSize) + i, y, _borderColor);
                    }
                }
            }

            pictureBox.Image = _bitmap;
        }

        public void SetQueueUICommand(Queue<ICycleInstruction> queueUICommands)
        {
            _queueInstructionUI = queueUICommands;
        }

        internal void SetController(Controller controller)
        {
            _controller = controller;
        }

        public void SetPauseCommand(PauseCommand command)
        {
            _pauseCommand = command;
        }

        public void SetRunCommand(RunCommand command)
        {
            _runCommand = command;
        }

        public void SetSaveCommand(SaveCommand command)
        {
            _saveCommand = command;
        }

        public void SetInitializationCommand(InitializationCommand command)
        {
            _initializationCommand = command;
        }

        public void SetColorAlgorithmCommand(ColorAlgorithmCommand command)
        {
            _colorAlgorithmCommand = command;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                _runCommand.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            DrawMap();

            buttonStop.Show();
            buttonRun.Hide();
            buttonPause.Enabled = true;

            textBoxQueueSize.Text = "0";

            _pause = false;
            _stop = false;
            _stopwatch.Start();

            _mapUpdate = new Thread(() =>
            {
                try
                {
                    while (_stop == false)
                    {
                        if (_pause == false)
                        {
                            Invoke((MethodInvoker)delegate { textBoxQueueSize.Text = _queueInstructionUI.Count().ToString(); });
                            Invoke((MethodInvoker)delegate { labelTime.Text = $"Время: {_stopwatch.Elapsed:hh\\:mm\\:ss}"; });

                            if (_queueInstructionUI.Count > 0)
                            {
                                ICycleInstruction instruction = _queueInstructionUI.Dequeue();

                                _instructionHandler.HandleRequest(instruction);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            _mapUpdate.Start();
        }

        public void DrawCell(int startX, int startY, Color color)
        {
            for (int x = startX; x < startX + CellSize; x++)
            {
                for (int y = startY; y < startY + CellSize; y++)
                {
                    _bitmap.SetPixel(x, y, color);
                }
            }
        }

        public void UpdateMap(UIUpdate uiUpdate)
        {
            int currentCycle = uiUpdate.Cycle;
            int agentsCount = uiUpdate.AgentsCount;

            // НУЖНО! Снять комментарий для нормальной отрисовки.
            lock (balanceLock)
            {
                if (_needDrawMap)
                    pictureBox.Image = _bitmap;
            }
            //

            Invoke((MethodInvoker)delegate { labelDayCounter.Text = $"Цикл: {currentCycle}"; });
            Invoke((MethodInvoker)delegate { labelAgentsCount.Text = $"Организмов: {agentsCount}"; });
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Вы уверены?", "Остановка симуляции", MessageBoxButtons.YesNo);
            if (result.ToString() == "Yes")
            {
                buttonRun.Show();
                buttonStop.Hide();
                buttonPause.Enabled = false;
                _stop = false;
                _runCommand.Undo();

                pictureBox.Image = null;


                _stopwatch.Reset();
                _mapUpdate.Abort();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            _pause = !_pause;
            if (_pause)
            {
                buttonPause.Text = "Продолжить";
                _stopwatch.Stop();
            }
            else
            {
                buttonPause.Text = "Пауза";
                _stopwatch.Start();
            }

        }

        private void scrollPanel_Scroll(object sender, ScrollEventArgs e)
        {
            // НУЖНО! Снять комментарий для нормальной отрисовки.
            lock (balanceLock)
            {
                pictureBox.Image = _bitmap;
            }
        }

        private void scrollPanel_Resize(object sender, EventArgs e)
        {
            // НУЖНО! Снять комментарий для нормальной отрисовки.
            lock (balanceLock)
            {
                pictureBox.Image = _bitmap;
            }
        }

        private void MapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _stop = true;
            _runCommand.Undo();
        }

        private void menuItemParametersInputForm_Click(object sender, EventArgs e)
        {
            ParametersInputForm form = new ParametersInputForm();
            form.SetMapForm(this);
            form.ShowDialog();
        }

        public void SetInitInfo(int countAgents, int maxX, int maxY)
        {
            XCountCells = maxX;
            YCountCells = maxY;

            _initializationCommand.CountAgents = countAgents;
            _initializationCommand.MaxX = maxX;
            _initializationCommand.MaxY = maxY;

            _initializationCommand.Execute();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                _needDrawMap = false;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
                _needDrawMap = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                _colorAlgorithmCommand.Undo();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                _colorAlgorithmCommand.ColorAlgorithm = ColorAlgorithms.EnergyGradient;
                _colorAlgorithmCommand.Execute();
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                _colorAlgorithmCommand.ColorAlgorithm = ColorAlgorithms.IntegrityGradient;
                _colorAlgorithmCommand.Execute();
            }

        }
    }
}
