using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Model
{
    /// <summary>
    /// Representation of a bomb on the map.
    /// </summary>
    public class Bomb : MapObject
    {
        /// <summary>
        /// Gets or sets the length of a bomb's explosion
        /// </summary>
        public int Range { get; set; }
        /// <summary>
        /// X component of the bomb's coordinates.
        /// </summary>
        public int PosX { get; set; }
        /// <summary>
        /// Y component of the bomb's coordinates.
        /// </summary>
        public int PosY { get; set; }
        /// <summary>
        /// Property to indicate whether the bomb has hit something.
        /// </summary>
        public bool Hit { get; set; }
        /// <summary>
        /// Reference to the bomb's owner (Player)
        /// </summary>
        public Player Owner { get; set; }

        /// <summary>
        /// Creates a bomb instance.
        /// </summary>
        /// <param name="range">Initial range</param>
        /// <param name="x">X coordinate (same as owner's)</param>
        /// <param name="y">Y coordinate (same as owner's)</param>
        /// <param name="owner">Reference to the bomb's owner (Player)</param>
        public Bomb(int range , int x, int y, Player owner)
        {
            this.PosX = x;
            this.PosY = y;
            this.Hit = false;
            this.Range = range;
            this.Owner = owner;
        }

        /// <summary>
        /// Creates a bomb instance.
        /// </summary>
        /// <param name="range">Initial range</param>
        public Bomb(int range)
        {
            this.Hit = false;
            this.Range = range;
        }
    }
}
