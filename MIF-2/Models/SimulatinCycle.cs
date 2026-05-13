using MIF2.Models.CycleInstructions;
using MIF2.Models.Agents;
using System;
using System.Collections.Generic;
using System.Threading;
using MIF2.Models.GenomeReaders;
using MIF2.Models.MIFMap;
using MIF2.Models.ColorAlgorithms;
using System.Drawing;
using MIF2.UI;
using System.IO;

namespace MIF2.Models
{
    class SimulationCycle
    {
        private int _seed = 0;
        private Map _map;
        private Thread _cycle = null;
        private GenomeReader _genomeReader;
        private List<Agent> _deadAgents;
        private ColorAlgorithm _colorAlgorithm;

        public int CycleCounter { get; private set; }
        public int StartCountAgents { get; private set; }
        public List<Agent> Agents { get; private set; }
        public Queue<ICycleInstruction> QueueInstructionUI { get; private set; }

        private Coordinates _oldAgentCoordinates;
        private Coordinates _newAgentCoordinates;

        public SimulationCycle(Map map)
        {
            _map = map;
            QueueInstructionUI = new Queue<ICycleInstruction>();
            
            Agents = new List<Agent>();
            _genomeReader = new GenomeReader(this);
            _deadAgents = new List<Agent>();
            _colorAlgorithm = new GreenWorld();
        }

        public void InitialSimulation(int countAgents)
        {
            StartCountAgents = countAgents;
            CycleCounter = 0;
            Agent agent;
            for (int i = 0; i < StartCountAgents; i++)
            {
                agent = new Agent(Constraints.DefaultAgentEnergy);
                agent.DefaultAgent();
                Agents.Add(agent);
            }
        }

        public void SetColorAlgorithm(ColorAlgorithm newAlgorithm)
        {
            _colorAlgorithm = newAlgorithm;

            ICycleInstruction[] instructions = new ICycleInstruction[Agents.Count];
            for (int i = 0; i < Agents.Count; i++)
            {
                instructions[i] = new ObjectMove(null, Agents[i].Coordinates, _colorAlgorithm.CalculateColor(Agents[i]));
            }

            UpdateColor(instructions);
        }

        private void PlaceAgentsOnMap()
        {
            foreach (Agent agent in Agents)
            {
                UpdateColor(agent);
            }
        }

        // Для тестов скорости симуляции.
        private MapForm _mapForm;
        internal void SetMapForm(MapForm mapForm)
        {
            _mapForm = mapForm;
        }

        private void UpdateColor(Agent agent)
        {
            if(_mapForm.NeedDrawMap())
            {
                QueueInstructionUI.Enqueue(new ObjectMove(null, agent.Coordinates, _colorAlgorithm.CalculateColor(agent)));
            }
        }

        private void UpdateColor(ICycleInstruction[] instructions)
        {
            if (_mapForm.NeedDrawMap())
            {
                foreach (ICycleInstruction instruction in instructions)
                {
                    QueueInstructionUI.Enqueue(instruction);
                }
            }
        }

        public void ScreenAgents()
        {
            Directory.CreateDirectory("Screens");
            string filePath = $"Screens\\c_{CycleCounter}.csv";

            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine("id,length,genome");
                foreach (Agent agent in Agents)
                {
                    byte[] g = agent._genome._genome;
                    string genomeStr = BitConverter.ToString(g).Replace("-", "");
                    writer.WriteLine($"{agent.Id},{g.Length},{genomeStr}");
                }
            }
        }

        /// <summary>
        /// Главный цикл симуляции. Позволяет всем агентам сделать ход, затем отсылает сигнал об окончании цикла.
        /// </summary>
        public void Start()
        {
            if (_cycle is null)
            {
                SaveParameters();

                _cycle = new Thread(() =>
                {
                    PlaceAgentsOnMap();
                    

                    while (Agents.Count > 0)
                    {
                        if (QueueInstructionUI.Count < Constraints.MaxSizeQueueInstruction)
                        {
                            for (int i = 0; i < Agents.Count; i++)
                            {
                                Agent agent = Agents[i];

                                if(agent.CanLive())
                                {
                                    _genomeReader.ExecuteGenome(agent);
                                    agent.NextCycle();

                                    if(_colorAlgorithm.NeedUpdateColor(CycleCounter))
                                    {
                                        UpdateColor(agent);
                                    }
                                }
                                else
                                {
                                    _deadAgents.Add(agent);
                                }
                                
                            }

                            foreach (Agent deadAgent in _deadAgents)
                            {
                                Agents.Remove(deadAgent);
                                _map.RemoveMIFObject(deadAgent.Coordinates);
                                ErasDeadAgent(deadAgent);
                            }
                            _deadAgents.Clear();

                            QueueInstructionUI.Enqueue(new UIUpdate(
                                CycleCounter,
                                Agents.Count,
                                CycleCounter % 100 == 0));

                            if (CycleCounter % Constraints.SaveEveryNTicks == 0)
                            {
                                ScreenAgents();
                            }

                            CycleCounter++;
                        }

                    }
                });

                _cycle.Start();
            }
        }

        public void SetOldCoordinates(Coordinates coordinates)
        {
            _oldAgentCoordinates = coordinates;
        }

        public void SetNewCoordinates(Coordinates coordinates)
        {
            _newAgentCoordinates = coordinates;
        }

        public void SendMoveInstruction(Agent agent)
        {
            if (_colorAlgorithm is GreenWorld && _oldAgentCoordinates == _newAgentCoordinates)
                return;

            if (_mapForm.NeedDrawMap())
            {
                ObjectMove om = new ObjectMove(_oldAgentCoordinates, _newAgentCoordinates, _colorAlgorithm.CalculateColor(agent));
                QueueInstructionUI.Enqueue(om);
            }
        }
        
        private void ErasDeadAgent(Agent deadAgent)
        {
            if (_mapForm.NeedDrawMap())
            {
                ObjectMove om = new ObjectMove(deadAgent.Coordinates, null, Color.Red);
                QueueInstructionUI.Enqueue(om);
            }
        }


        public void Stop()
        {
            _cycle?.Abort();
            _cycle = null;
            QueueInstructionUI.Clear();
            CycleCounter = 0;
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Continue()
        {
            throw new NotImplementedException();
        }

        private void SaveParameters()
        {
            Directory.CreateDirectory("Screens");
            MIFEnvironment env = _map.GetCell(0, 0).MIFEnvironment;

            var parameters = new SimulationParameters
            {
                StartedAt = DateTime.Now,
                RandomSeed = _seed,

                MapWidth = _map.Width,
                MapHeight = _map.Height,
                InitialAgentsCount = Agents.Count,

                // Параметры среды берутся из любой клетки — они однородные.
                // Когда среда станет неоднородной, эту часть надо переделать.
                
                Illumination = env.Illumination,
                Mutagenicity = env.Mutagenicity,
                Density = env.Density,
                Temperature = env.Temperature,

                DefaultGenomeSize = Constraints.DefaultGenomeSize,
                MaxGenomeSize = Constraints.MaxGenomeSize,
                MaxAgentAge = Constraints.MaxAgentAge,
                DefaultAgentIntegrity = Constraints.DefaultAgentIntegrity,
                DefaultAgentEnergy = Constraints.DefaultAgentEnergy,

                BasePhotosynthesisEnergy = Constraints.BasePhotosynthesisEnergy,
                BaseEverydayEnergy = Constraints.BaseEverydayEnergy,
                BaseAttackEnergy = Constraints.BaseAttackEnergy,
                BaseMitosisEnergy = Constraints.BaseMitosisEnergy,
                BaseMoveEnergy = Constraints.BaseMoveEnergy,
                BaseDamage = Constraints.BaseDamage,

                SaveEveryNTicks = Constraints.SaveEveryNTicks
            };

            parameters.SaveToFile("Screens\\simulation_params.json");
        }


    }
}
