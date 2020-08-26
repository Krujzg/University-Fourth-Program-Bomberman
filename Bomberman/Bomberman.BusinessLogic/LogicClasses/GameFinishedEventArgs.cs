// <copyright file="GameFinishedEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic.LogicClasses
{
    using System;
    using System.Diagnostics;
    using Bomberman.Model;

    /// <summary>
    /// Gamefinished event, contains winner and looser properties
    /// </summary>
    public class GameFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameFinishedEventArgs"/> class.
        /// </summary>
        /// <param name="p1">winner</param>
        /// <param name="p2">looser</param>
        /// <param name="gametime">gametime</param>
        public GameFinishedEventArgs(Player p1, Player p2, int gametime)
        {
            this.WinnerName = p1.Name;
            this.WinnerPoints = p1.Score;
            this.WinnerColor = p1.Color;
            this.WinnerBombCount = p1.Bombs.Count;
            this.LoserName = p2.Name;
            this.LoserPoints = p2.Score;
            this.LoserColor = p2.Color;
            this.LoserBombCount = p2.Bombs.Count;
            this.Gametime = gametime;
        }

        /// <summary>
        /// Gets or sets winner name
        /// </summary>
        public string WinnerName { get; set; }

        /// <summary>
        /// Gets or sets winner points
        /// </summary>
        public int WinnerPoints { get; set; }

        /// <summary>
        /// Gets or sets winner color
        /// </summary>
        public string WinnerColor { get; set; }

        /// <summary>
        /// Gets or sets winner bombcount
        /// </summary>
        public int WinnerBombCount { get; set; }

        /// <summary>
        /// Gets or sets looser name
        /// </summary>
        public string LoserName { get; set; }

        /// <summary>
        /// Gets or sets looser points
        /// </summary>
        public int LoserPoints { get; set; }

        /// <summary>
        /// Gets or sets looser color
        /// </summary>
        public string LoserColor { get; set; }

        /// <summary>
        /// Gets or sets loosers bombcount
        /// </summary>
        public int LoserBombCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets draw
        /// </summary>
        public bool Draw { get; set; }

        /// <summary>
        /// Gets or sets gametime
        /// </summary>
        public int Gametime { get; set; }
    }
}
