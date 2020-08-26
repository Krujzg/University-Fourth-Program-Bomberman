// <copyright file="GameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Bomberman.BusinessLogic;
    using Bomberman.BusinessLogic.Enum;
    using Bomberman.BusinessLogic.LogicClasses;
    using Bomberman.Data;
    using Bomberman.Repository;

    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameLogic gL;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow"/> class.
        /// Sets the gamingfield
        /// </summary>
        /// <param name="filePath">Path of the file loaded</param>
        /// <param name="p1_name">player 1 s name</param>
        /// <param name="p2_name">player 2 s name</param>
        public GameWindow(string filePath, string p1_name, string p2_name)
        {
            this.InitializeComponent();
            this.gL = this.FindResource("logic") as GameLogic;

            if (filePath.Contains(".sav"))
            {
                this.gL.LoadGameLogic(filePath);
            }
            else
            {
                this.gL.SetupGameLogic(filePath, p1_name, p2_name);
            }

            this.gL.GameFinished += this.GL_GameFinished;
        }

        /// <summary>
        /// Runs after someone died, show highschore , winner and gametime
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GL_GameFinished(object sender, EventArgs e)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                GameFinishedEventArgs gamefinished = e as GameFinishedEventArgs;
                HighScoreWindow highScoreWindow = new HighScoreWindow();
                MessageBox.Show(
                    "A nyertes neve " + gamefinished.WinnerName + " pontjai: " + gamefinished.WinnerPoints + " pts" +
                    "\nA vesztes neve: " + gamefinished.LoserName + " pontjai: " + gamefinished.LoserPoints + " pts" +
                    "\nA játékidő: " + gamefinished.Gametime.ToString() + " mp volt");
                highScoreWindow.ShowDialog();
                this.gL.SoundSystem.PlayGameStartSound();
                this.Close();
            });
        }

        /// <summary>
        /// esc key will exit the game and also saves it
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBox.Show("A játékállás mentésre került.");
                this.gL.SaveGame();
                this.gL.SoundSystem.PlayGameStartSound();
                this.Close();
            }

            if (e.Key == Key.W)
            {
                this.gL.Control(Movement.Up, this.gL.Players[0]);
            }
            else if (e.Key == Key.S)
            {
                this.gL.Control(Movement.Down, this.gL.Players[0]);
            }
            else if (e.Key == Key.A)
            {
                this.gL.Control(Movement.Left, this.gL.Players[0]);
            }
            else if (e.Key == Key.D)
            {
                this.gL.Control(Movement.Right, this.gL.Players[0]);
            }
            else if (e.Key == Key.G)
            {
                this.gL.Control(Movement.DropBomb, this.gL.Players[0]);
            }
            else if (e.Key == Key.Up)
            {
                this.gL.Control(Movement.Up, this.gL.Players[1]);
            }
            else if (e.Key == Key.Down)
            {
                this.gL.Control(Movement.Down, this.gL.Players[1]);
            }
            else if (e.Key == Key.Left)
            {
                this.gL.Control(Movement.Left, this.gL.Players[1]);
            }
            else if (e.Key == Key.Right)
            {
                this.gL.Control(Movement.Right, this.gL.Players[1]);
            }
            else if (e.Key == Key.NumPad6 || e.Key == Key.Space)
            {
                this.gL.Control(Movement.DropBomb, this.gL.Players[1]);
            }
        }
    }
}
