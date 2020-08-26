// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic
{
    /// <summary>
    /// This is a GameModel's interface, which will create the map and the players. It can also store and save games
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// This method is building the playground (map)
        /// </summary>
        /// <param name="filePath"> It is the path of the saved file</param>
        void GetMap(string filePath);
    }
}
