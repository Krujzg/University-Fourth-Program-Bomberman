using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Model
{
    /// <summary>
    /// Representation of a wall or barrel on the map (depends on the Destroyable property).
    /// </summary>
    public class Wall : MapObject
    {
        /// <summary>
        /// Decides whether the wall is solid or is a barrel (destroyable).
        /// </summary>
        public bool Destroyable { get; set; }

        /// <summary>
        /// Creates an instance of a wall or a barrel.
        /// </summary>
        /// <param name="destroyable">Initializes Destroyable property</param>
        public Wall(bool destroyable)
        {
            this.Destroyable = destroyable;
        }
    }
}
