using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Models
{
    public class Wall
    {
        public bool Destroyable { get; set; }

        public Wall(bool destroyable)
        {
            this.Destroyable = destroyable;
        }
    }
}
