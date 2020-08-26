using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Model
{
    /// <summary>
    /// Representation of a player on the map.
    /// </summary>
    public class Player : MapObject
    {
        /// <summary>
        /// List of droppable bombs
        /// </summary>
        public List<Bomb> Bombs { get; set; }

        /// <summary>
        /// Indicates if the player got hit
        /// </summary>
        public bool GotHit { get; set; }

        /// <summary>
        /// Collection of powerups
        /// </summary>
        public ObservableCollection<PowerUp> PowerUps { get; set; }

        /// <summary>
        /// Gets or sets actual player's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets actual player's color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// X comonent of player's coordinates
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// Y comonent of player's coordinates
        /// </summary>
        public int PosY { get; set; }

        /// <summary>
        /// Gets or sets the amount of points the player has
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the player's speed
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Changes the range of a player's bombs
        /// </summary>
        public int BombRange { get; set; }

        /// <summary>
        /// Creates an instance of a Player
        /// </summary>
        /// <param name="x">X comonent of player's coordinates</param>
        /// <param name="y">Y comonent of player's coordinates</param>
        /// <param name="name">Name of the player</param>
        /// <param name="color">Color of the player</param>
        public Player(int x, int y, string name, string color)
        {
            this.PosX = x;
            this.PosY = y;
            this.Score = 0;
            this.BombRange = 1;
            this.Name = name;
            this.Color = color;
            Bombs = new List<Bomb>
            {
                new Bomb(1)
            };
            PowerUps = new ObservableCollection<PowerUp>();
        }
    }
}
