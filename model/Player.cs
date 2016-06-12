using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class Player
    {
        private int m_x;
        private int m_y;

        public Player(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public int x
        {
            get
            {
                return m_x;
            }
            set
            {
                m_x = value;
            }
        }
        public int y
        {
            get
            {
                return m_y;
            }
            set
            {
                m_y = value;
            }
        }
    }
}
