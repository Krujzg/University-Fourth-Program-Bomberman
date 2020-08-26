// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bomberman.Model;

    /// <summary>
    /// This is a GameModel, which will create the map and the players. It can also store and save games
    /// </summary>
    public class GameModel : IGameModel
    {
        private static Random rnd = new Random();

        /// <summary>
        /// Gets or sets the players in the game
        /// </summary>
        public virtual Player[] Players { get; set; } = new Player[2];

        /// <summary>
        /// Gets or sets the map in the game
        /// </summary>
        public virtual MapObject[,] Map { get; set; }

        /// <summary>
        /// This method is building the playground (map)
        /// </summary>
        /// <param name="filePath"> It is the path of the saved file</param>
        public void GetMap(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int sideLength = lines.Length;

            this.Map = new MapObject[sideLength, sideLength];

            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    this.Map[i, j] = this.ConvertFromChar(lines[i][j], i, j);
                }
            }
        }

        /// <summary>
        /// This method is Loads a Game from txt
        /// </summary>
        /// <param name="filePath"> This is a txt-s filepath </param>
        public void LoadGame(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int sideLength = lines.Length;
            string[] p1_data = lines[0].Split(';');
            string[] p2_data = lines[1].Split(';');

            this.Map = new MapObject[sideLength - 2, sideLength - 2];

            for (int i = 2; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength - 2; j++)
                {
                    this.Map[i - 2, j] = this.ConvertFromChar(lines[i][j], i - 2, j);
                }
            }

            this.Players[0].Name = p1_data[0];
            this.Players[0].Score = Convert.ToInt32(p1_data[1]);
            int p1_bombcount = Convert.ToInt32(p1_data[2]);

            for (int i = 0; i < p1_bombcount; i++)
            {
                this.Players[0].Bombs.Add(new Bomb(1));
            }

            this.Players[1].Name = p2_data[0];
            this.Players[1].Score = Convert.ToInt32(p2_data[1]);
            int p2_bombcount = Convert.ToInt32(p2_data[2]);

            for (int i = 0; i < p2_bombcount; i++)
            {
                this.Players[1].Bombs.Add(new Bomb(1));
            }
        }

        /// <summary>
        /// This method convert a charachter from the txt to Mapobjects
        /// </summary>
        /// <param name="c"> charachter from the txt </param>
        /// <param name="x"> x coordinate</param>
        /// <param name="y"> y coordinate</param>
        /// <returns>The MapObjext converted from the input charachter</returns>
        public MapObject ConvertFromChar(char c, int x, int y)
        {
            switch (c)
            {
                case '*':
                    return new Wall(false);
                case '_':
                    return new Floor();
                case '&':
                    return new Wall(true);
                case ' ':
                    return this.GetWallOrFloor();
                case '1':
                    this.Players[0] = new Player(y, x, "p1", "blue");
                    return this.Players[0];
                default:
                    this.Players[1] = new Player(y, x, "p2", "red");
                    return this.Players[1];
            }
        }

        /// <summary>
        /// This method is making a txt file from the current map
        /// </summary>
        /// <param name="mapObject"> This method gets a mapobject and decides its type</param>
        /// <returns>It returns a string to be written in the txt</returns>
        public string ConvertToString(MapObject mapObject)
        {
            if (mapObject is Player)
            {
                if ((mapObject as Player).Color == "blue")
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            else if (mapObject is Floor)
            {
                return "_";
            }
            else if (mapObject is Wall)
            {
                if ((mapObject as Wall).Destroyable)
                {
                    return "&";
                }
                else
                {
                    return "*";
                }
            }

            return "_";
        }

        /// <summary>
        /// This method creating a savedgame txt from the current map
        /// </summary>
        public void SaveGame()
        {
            string[,] charachters = new string[this.Map.GetLength(0), this.Map.GetLength(1)];
            string[] lines = new string[this.Map.GetLength(0) + 2];
            lines[0] = this.Players[0].Name + ";" + this.Players[0].Score + ";" + this.Players[0].Bombs.Count;
            lines[1] = this.Players[1].Name + ";" + this.Players[1].Score + ";" + this.Players[1].Bombs.Count;
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    charachters[i, j] = this.ConvertToString(this.Map[i, j]);
                    lines[i + 2] = lines[i + 2] + charachters[i, j];
                }
            }

            string directory = Directory.GetParent(
                Directory.GetParent(
                    Environment.CurrentDirectory).ToString())
                    .ToString() + "/SavedGames/";
            string dt = DateTime.Now.ToString() + ".sav";
            dt = dt.Replace(':', '-');
            dt = dt.Replace('/', '-');
            directory = directory.Replace('\\', '/') + dt;
            File.WriteAllLines(directory, lines);
        }

        /// <summary>
        /// This method makes a barrel or a floor randomly
        /// </summary>
        /// <returns> barrel or floor </returns>
        private MapObject GetWallOrFloor()
        {
            if (rnd.Next(0, 2) == 1)
            {
                return new Wall(true);
            }
            else
            {
                return new Floor();
            }

            // return new Floor();

            // return rnd.Next(0, 2) != 1 ? new Floor() : new Wall();
        }
    }
}
