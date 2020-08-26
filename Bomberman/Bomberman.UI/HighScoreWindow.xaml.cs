// <copyright file="HighScoreWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Bomberman.BusinessLogic;
    using Bomberman.Data;
    using Bomberman.Repository;

    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        private DataBaseLogic dataBaseLogic = new DataBaseLogic(new Repository());

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreWindow"/> class.
        /// Datacontext will be the database though the databaselogic s select
        /// </summary>
        public HighScoreWindow()
        {
            this.InitializeComponent();
            List<Rounds> rounds = this.dataBaseLogic.Select(new Rounds()).ToList();
            this.DataContext = rounds;
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Close(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.Close();
        }
    }
}
