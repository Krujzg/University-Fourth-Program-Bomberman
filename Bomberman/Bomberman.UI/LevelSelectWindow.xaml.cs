// <copyright file="LevelSelectWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Bomberman.UI.ViewModels;

    /// <summary>
    /// Interaction logic for LevelSelectWindow.xaml
    /// </summary>
    public partial class LevelSelectWindow : Window
    {
        private LevelSelectViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSelectWindow"/> class.
        /// </summary>
        public LevelSelectWindow()
        {
            this.InitializeComponent();
            this.vm = this.FindResource("VM") as LevelSelectViewModel;
            this.DataContext = this.vm;
        }

        /// <summary>
        /// Load a game
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LoadGame(object sender, RoutedEventArgs e)
        {
            var levelpath = (sender as Button).Tag.ToString();
            GameWindow gw = new GameWindow(levelpath, this.vm.P1, this.vm.P2);
            this.Close();
            gw.ShowDialog();
        }

        /// <summary>
        /// escape
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Clear player textbox after 2 clicks on them
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PlayertextClear(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).Clear();
        }
    }
}
