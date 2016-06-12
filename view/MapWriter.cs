using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class MapWriter
    {
        static public void save(Map map, string filename)
        {
            throw new IOException("saving to file: " + filename + " failed");
        }

        static public Map load(string filename)
        {
            throw new IOException("load from file: " + filename + " failed");
            //return new Map("empty", 100, 100);
        }
    }
}
