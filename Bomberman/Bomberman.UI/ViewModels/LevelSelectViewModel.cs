// <copyright file="LevelSelectViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bomberman.UI.Helpers;

    /// <summary>
    /// This class helps to load a default map, or a savedgame state
    /// </summary>
    public class LevelSelectViewModel : Bindable
    {
        /// <summary>
        /// Gets all the saved games
        /// </summary>
        private readonly FileInfo[] loadGameFiles = new DirectoryInfo(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()) + "\\SavedGames").GetFiles("*.sav");

        /// <summary>
        /// Gets all the default maps
        /// </summary>
        private readonly FileInfo[] newGameFiles = new DirectoryInfo(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()) + "\\Maps").GetFiles("*.txt");

        /// <summary>
        /// Gets the default files ordered by name
        /// </summary>
        public IOrderedEnumerable<FileInfo> NewGameFiles
        {
            get
            {
                return this.newGameFiles.OrderBy(y => y.Name);
            }
        }

        /// <summary>
        /// Gets the saved files ordered by name
        /// </summary>
        public IOrderedEnumerable<FileInfo> LoadGameFiles
        {
            get
            {
                return this.loadGameFiles.OrderByDescending(y => y.Name);
            }
        }

        /// <summary>
        /// Gets or sets the player one s name
        /// </summary>
        public string P1 { get; set; } = "Player 1";

        /// <summary>
        /// Gets or sets the player two s name
        /// </summary>
        public string P2 { get; set; } = "Player 2";
    }
}
