using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Models
{
    public class Bomb
    {
        public int Range { get; set; }
        public bool Hit { get; set; }

        public Bomb(int range)
        {
            this.Hit = false;
            this.Range = range;
        }
    }
}
