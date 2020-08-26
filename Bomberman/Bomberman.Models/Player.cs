using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Models
{
    public class Player
    {
        public List<Bomb> Bombs { get; set; }

        public bool GotHit { get; set; }

        public ObservableCollection<PowerUp> PowerUps { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public int Score { get; set; }

        public int Speed { get; set; }

        public int BombRange { get; set; }

        public Player(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
            this.Score = 0;
            this.BombRange = 1;
            Bombs = new List<Bomb>();
            Bombs.Add(new Bomb(1));
            PowerUps = new ObservableCollection<PowerUp>();      
        }
    }
}
