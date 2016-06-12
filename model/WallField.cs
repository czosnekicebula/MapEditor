using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class WallField : Field
    {
        public WallField() : base(Field.Type.FIELD_TYPE_WALL)
        {
            //loadTexture("wall.png")
        }

        public override bool isPassable()
        {
            return false;   // can't pass thru walls yet
        }
    }
}
