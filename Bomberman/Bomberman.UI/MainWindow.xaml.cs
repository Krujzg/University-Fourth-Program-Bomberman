// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Bomberman.BusinessLogic;
    using Bomberman.Data;
    using Bomberman.Repository;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameLogic gl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.gl = new GameLogic();
            this.gl.SoundSystem.PlayGameStartSound();
        }

        /// <summary>
        /// Close with esc key
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Start game button click opens the levelselectwindow
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void StartGame(object sender, RoutedEventArgs e)
        {
            LevelSelectWindow levelSelectWindow = new LevelSelectWindow();
            levelSelectWindow.ShowDialog();
        }

        /// <summary>
        /// Start game button click opens the instructionwindow
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Instruction(object sender, RoutedEventArgs e)
        {
            InstructionsWindow instructionsWindow = new InstructionsWindow();
            instructionsWindow.Show();
        }

        /// <summary>
        /// Start game button click opens the highscorewindow
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void HighScore(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highScoreWindow = new HighScoreWindow();
            highScoreWindow.ShowDialog();
        }

        /// <summary>
        /// exit button click exits the progam
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
