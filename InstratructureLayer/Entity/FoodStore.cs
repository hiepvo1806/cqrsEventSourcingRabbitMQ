using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.Entity
{
    public class FoodStore : BaseEntity<Guid>
    {
        public string Link { get; set; }
        public string Name { get; set; }

    }
}
