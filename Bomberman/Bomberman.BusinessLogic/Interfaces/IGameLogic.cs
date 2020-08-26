namespace Bomberman.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bomberman.Model;

    public interface IGameLogic
    {
        void Up(Player player);

        void Down(Player player);

        void Left(Player player);

        void Right(Player player);

        void DropBomb(Player player);

        void RegenerateBomb(Player player);

        void PickUpPowerUp(Player player, PowerUp powerup);

        void GetPoints(Player player, PointTypes point_types);
    }
}
