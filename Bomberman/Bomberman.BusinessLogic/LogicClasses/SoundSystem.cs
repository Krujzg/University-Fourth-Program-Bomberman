// <copyright file="SoundSystem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic.LogicClasses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Media;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class helps playing sounds
    /// </summary>
    public class SoundSystem
    {
        private readonly string directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundSystem"/> class.
        /// </summary>
        public SoundSystem()
        {
            this.directory = Directory.GetParent(
               Directory.GetParent(
                   Environment.CurrentDirectory).ToString())
                   .ToString() + "/Sounds/";
        }

        /// <summary>
        /// play gamestart sound
        /// </summary>
        public void PlayGameStartSound()
        {
            try
            {
                SoundPlayer gamestart = new SoundPlayer(this.directory + "Gamestart.wav");
                gamestart.Play();
            }
            catch
            {
            }
        }

        /// <summary>
        /// play explodebomb sound
        /// </summary>
        public void PlayExplodeBombSound()
        {
            try
            {
                SoundPlayer gamestart = new SoundPlayer(this.directory + "explodebomb.wav");
                gamestart.Play();
            }
            catch
            {
            }
        }

        /// <summary>
        /// play endgamesound sound
        /// </summary>
        public void PlayEndGameSound()
        {
            try
            {
                SoundPlayer gamestart = new SoundPlayer(this.directory + "endgame.wav");
                gamestart.Play();
            }
            catch
            {
            }
        }

        /// <summary>
        /// play TADA sound
        /// </summary>
        public void PlayTADASound()
        {
            try
            {
                SoundPlayer gamestart = new SoundPlayer(this.directory + "TADA.wav");
                gamestart.Play();
            }
            catch
            {
            }
        }
    }
}
