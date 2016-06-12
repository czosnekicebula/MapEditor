using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class FloorField : Field
    {
        public FloorField() : base(Field.Type.FIELD_TYPE_FLOOR)
        {
            //loadTexture("floor.png")
        }

        public override bool isPassable()
        {
            return true;   // walk on the floor normally
        }
    }
}
