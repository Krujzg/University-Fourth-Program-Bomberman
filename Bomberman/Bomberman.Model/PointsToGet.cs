using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Model
{
    /// <summary>
    /// Represents all the different scenarios when a player can earn points.
    /// </summary>
    public enum PointTypes
    {
        /// <summary>
        /// Player gets points when it destroys a wall.
        /// </summary>
        DestroyWall,
        /// <summary>
        /// Player gets points when it Picks up a powerup.
        /// </summary>
        PickUpPowerUps,
        /// <summary>
        /// Player gets points when it kills another player.
        /// </summary>
        KillOtherPlayer
    }
}
