using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class MainWindow : Form
    {
        private Map map;

        public MainWindow()
        {
            InitializeComponent();
            this.map = new Map("untitled map", 32, 32);
            m_mapPanel.setMap(map);

            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(keyPressEvent);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.map = new Map("new", 32, 32);
            this.m_mapPanel.setMap(map);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Map file (*.map)|*.map|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Console.WriteLine("Loading map file from: " + openFileDialog1.FileName);
                    this.map = MapWriter.load(openFileDialog1.FileName);
                    this.m_mapPanel.setMap(map);
                    Console.WriteLine("Loading finished");
                }
                catch (IOException err)
                {
                    Console.WriteLine("Loading file failed: " + err.Message);
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Filter = "Map file (*.map)|*.map|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Console.WriteLine("Saving map file to: " + saveFileDialog1.FileName);
                    MapWriter.save(map, saveFileDialog1.FileName);
                    Console.WriteLine("Saving finished");
                }
                catch (IOException err)
                {
                    Console.WriteLine("Saving file failed: " + err.Message);
                }
            }
        }

        private void wallButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_wallButton.Checked)
                m_mapPanel.chosenType = Field.Type.FIELD_TYPE_WALL;
        }
        private void floorButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_floorButton.Checked)
                m_mapPanel.chosenType = Field.Type.FIELD_TYPE_FLOOR;
        }
        private void furnitureButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_furnitureButton.Checked)
                m_mapPanel.chosenType = Field.Type.FIELD_TYPE_FURNITURE;
        }

        private void keyPressEvent(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'd':
                    if (map.player.x < map.width-1                                      // is the field to the right a valid one (within dimensions)
                        && map.getField(map.player.x+1, map.player.y).isPassable())     // and is it passable
                        map.player.x++;
                    e.Handled = true;
                    m_mapPanel.Refresh();
                    break;
                case 'a':
                    if (map.player.x > 0
                        && map.getField(map.player.x - 1, map.player.y).isPassable())
                        map.player.x--;
                    e.Handled = true;
                    m_mapPanel.Refresh();
                    break;
                case 'w':
                    if (map.player.y > 0
                        && map.getField(map.player.x, map.player.y-1).isPassable())
                        map.player.y--;
                    e.Handled = true;
                    m_mapPanel.Refresh();
                    break;
                case 's':
                    if (map.player.y < map.height - 1
                        && map.getField(map.player.x, map.player.y + 1).isPassable())
                        map.player.y++;
                    e.Handled = true;
                    m_mapPanel.Refresh();
                    break;
            }
        }
    }
}
