using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor
{
    class MapPanel : Panel, IMessageFilter
    {
        private const int FIELD_DRAW_SIZE = 25;
        private Map m_map;

        private Dictionary<Field.Type, SolidBrush> m_brushesList;

        private SolidBrush m_playerBrush = new SolidBrush(Color.Blue);

        private Field.Type m_chosenType = Field.Type.FIELD_TYPE_UNKNOWN;

        public MapPanel()
        {
            Application.AddMessageFilter(this);
            initBrushesList();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapPanel_Paint);

            m_map = null;
        }
        private void initBrushesList()
        {
            m_brushesList = new Dictionary<Field.Type, SolidBrush>{
                { Field.Type.FIELD_TYPE_WALL, new SolidBrush(Color.Black) },
                { Field.Type.FIELD_TYPE_FLOOR, new SolidBrush(Color.White) } ,
                { Field.Type.FIELD_TYPE_FURNITURE, new SolidBrush(Color.Gray) }
            };
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) Application.RemoveMessageFilter(this);
            base.Dispose(disposing);
        }

        public void setMap(Map map)
        {
            this.m_map = map;
            //this.Size = new Size(map.width * FIELD_DRAW_SIZE, map.height * FIELD_DRAW_SIZE);
            this.Refresh();
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.HWnd == this.Handle)     
            {
                if (m.Msg == 0x201)         
                {                           // Trap WM_LBUTTONDOWN   
                    Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);

                    if (pos.X / FIELD_DRAW_SIZE < m_map.width 
                        && pos.Y / FIELD_DRAW_SIZE < m_map.height)
                    {
                        Field newField = null;
                        switch(m_chosenType)
                        {
                            case Field.Type.FIELD_TYPE_FLOOR:
                                newField = new FloorField();
                                break;
                            case Field.Type.FIELD_TYPE_FURNITURE:
                                newField = new FurnitureField();
                                break;
                            case Field.Type.FIELD_TYPE_WALL:
                                newField = new WallField();
                                break;
                            case Field.Type.FIELD_TYPE_UNKNOWN:
                                return false;   // cannot determine new field type, jump out
                        }

                        this.m_map.setField(pos.X / FIELD_DRAW_SIZE, pos.Y / FIELD_DRAW_SIZE, newField);
                        this.Refresh();
                    }

                    return true;            // konsumujesz
                }
            }
            return false;
        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            if (m_map == null)
                return;

            using (Graphics g = this.CreateGraphics())
            {
                for (int i = 0; i < m_map.width; i++)
                {
                    for (int j = 0; j < m_map.height; j++)
                    {
                        SolidBrush brush = null;
              
                        if (this.m_map.player.x == i && this.m_map.player.y == j)
                        {
                            brush = m_playerBrush;
                        }
                        else
                        {
                            brush = m_brushesList[this.m_map.getField(i, j).type];
                        }

                        g.FillRectangle(
                            brush,
                            i * FIELD_DRAW_SIZE,
                            j * FIELD_DRAW_SIZE,
                            FIELD_DRAW_SIZE,
                            FIELD_DRAW_SIZE);
                    }
                }
            }
        }

        public Field.Type chosenType
        {
            get
            {
                return m_chosenType;
            }
            set
            {
                m_chosenType = value;
            }
        }

    }
}
