using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MIF2.Models.MIFMap;
using MIF2.Models.Agents;

namespace MIF2.Models.MIFMap
{
    class Map
    {
        private int _width;
        private int _height;
        private Cell[,] _map;

        public Map(int width, int height)
        {
            _width = width;
            _height = height;

            InitialMap();
            EnvironmentGeneration();
        }

        private void InitialMap()
        {
            _map = new Cell[_width, _height];
            for (int h = 0; h < _height; h++)
            {
                for (int w = 0; w < _width; w++)
                {
                    _map[w, h] = new Cell();
                }
            }
        }

        public void EnvironmentGeneration()
        {
            for (int h = 0; h < _height; h++)
            {
                for (int w = 0; w < _width; w++)
                {
                    _map[w, h].MIFEnvironment = new MIFEnvironment();
                    //_map[w, h].MIFEnvironment.Illumination = 0;
                    //_map[w, h].MIFEnvironment.Mutagenicity = 0;
                    //_map[w, h].MIFEnvironment.Density = 0;
                    //_map[w, h].MIFEnvironment.Temperature = 0;
                }
            }
        }

        public void AddAgents(List<Agent> agents)
        {
            Randomizer rnd = new Randomizer();
            foreach (Agent agent in agents)
            {
                agent.Coordinates = new Coordinates(rnd.Next() % _width, rnd.Next() % _height);
                _map[agent.Coordinates.X, agent.Coordinates.Y].MIFObject = agent;
                agent.AddMap(this);
            }
        }

        public Coordinates CalculateCoordinates(Coordinates coordinates, Vector vector)
        {
            // Мир замкнут на себя по оси X
            int newX = coordinates.X + vector.DeltaX;
            if (newX < 0)
                newX += _width;
            if (newX >= _width)
                newX -= _width;

            // Мир замкнут на себя по оси Y
            int newY = coordinates.Y + vector.DeltaY;
            if (newY < 0)
                newY += _height;
            if (newY >= _height)
                newY -= _height;

            return new Coordinates(newX, newY);
        }

        public Coordinates Move(Coordinates coordinates, Vector vector)
        {
            Coordinates newCoordinates = CalculateCoordinates(coordinates, vector);

            Cell currentCell = _map[coordinates.X, coordinates.Y];
            Cell newCell = _map[newCoordinates.X, newCoordinates.Y];

            if (newCell.MIFObject is null)
            {
                newCell.MIFObject = currentCell.MIFObject;
                currentCell.RemoveMIFObject();
                return newCoordinates;
            }
            else
            {
                return coordinates;
            }
        }

        public void SetMIFObject(IMIFObject obj, Coordinates coordinates)
        {
            _map[coordinates.X, coordinates.Y].MIFObject = obj;
        }

        public IMIFObject GetMIFObject(Coordinates coordinates)
        {
            return _map[coordinates.X, coordinates.Y].MIFObject;
        }

        public IMIFObject GetMIFObject(Coordinates coordinates, Vector vector)
        {
            Coordinates objectCoordinates = CalculateCoordinates(coordinates, vector);
            return GetMIFObject(objectCoordinates);
        }

        public void RemoveMIFObject(Coordinates coordinates)
        {
            _map[coordinates.X, coordinates.Y].RemoveMIFObject();
        }

        internal Cell GetCell(Coordinates coordinates)
        {
            return _map[coordinates.X, coordinates.Y];
        }
    }
}
