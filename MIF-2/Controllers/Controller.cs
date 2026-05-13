using MIF2.Models.MIFMap;
using MIF2.Models;
using MIF2.UI;
using MIF2.UI.UICommands;
using System;
using System.Runtime.CompilerServices;
using MIF2.Models.ColorAlgorithms;
using System.IO;
using MIF2.Models.Agents;

namespace MIF2.Controllers
{
    public enum Status { Error }

    class Controller
    {
        private MapForm _mapForm;
        private SimulationCycle _cycle;
        private Map _map;

        public Controller()
        {

        }

        public void SetMapForm(MapForm mapForm)
        {
            _mapForm = mapForm;
            PrepMapForm();
        }

        public void InitialSimulation(int countAgents, int maxX, int maxY)
        {
            _map = new Map(maxX, maxY);
            _cycle = new SimulationCycle(_map);

            _cycle.InitialSimulation(countAgents);
            _map.AddAgents(_cycle.Agents);

            _mapForm.SetQueueUICommand(_cycle.QueueInstructionUI);
            _mapForm.SetController(this);


            _cycle.SetMapForm(_mapForm); // Для тестов скорости симуляции.
        }

        private void PrepMapForm()
        {
            _mapForm.SetPauseCommand(new PauseCommand(this));
            _mapForm.SetRunCommand(new RunCommand(this));
            _mapForm.SetSaveCommand(new SaveCommand(this));
            _mapForm.SetInitializationCommand(new InitializationCommand(this));
            _mapForm.SetColorAlgorithmCommand(new ColorAlgorithmCommand(this));
        }

        public void Start()
        {
            if(_cycle is null)
            {
                throw new Exception("Ошибка: Параметры симуляции не выбраны.");
            }
            
            _cycle.Start();
        }

        public void Stop()
        {
            _cycle?.Stop();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Continue()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SetColorAlgorithm(ColorAlgorithms сolorAlgorithm)
        {
            switch (сolorAlgorithm)
            {
                case ColorAlgorithms.GreenWorld:
                    _cycle?.SetColorAlgorithm(new GreenWorld());
                    break;

                case ColorAlgorithms.EnergyGradient:
                    _cycle.SetColorAlgorithm(new GradientEnergy());
                    break;

                case ColorAlgorithms.IntegrityGradient:
                    _cycle.SetColorAlgorithm(new GradientIntegrity());
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
