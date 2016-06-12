using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    abstract class Field
    {
        // define the type of this enum - can be either, unknown if not set
        public enum Type
        {
            FIELD_TYPE_FLOOR,
            FIELD_TYPE_WALL,
            FIELD_TYPE_FURNITURE,

            FIELD_TYPE_UNKNOWN
        }

        private Type m_type = Type.FIELD_TYPE_UNKNOWN;

        // ctor
        public Field(Type type)
        {
            this.type = type;
        }

        // properties
        public Type type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        public abstract bool isPassable();

        protected void loadTexture(string textureFilename)
        {
            // wczytaj teksture pola
        }
    }
}
