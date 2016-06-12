using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class FurnitureField : Field
    {
        public FurnitureField() : base(Field.Type.FIELD_TYPE_FURNITURE)
        {
            //loadTexture("furniture.png")
        }

        public override bool isPassable()
        {
            return false;   // dont walk on furniture!
        }
    }
}
