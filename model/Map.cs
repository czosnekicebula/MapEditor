namespace MapEditor
{
    class Map
    {
        // class variables
        private static string MAP_FILE_EXTENSION = ".map";

        // object variables
        private string m_name;          // nazwa - uzyta do zapisu?
        private int m_width;            // szerokosc macierzy pol
        private int m_height;           // wysokosc macierzy pol
        Field[,] m_fieldMatrix;           // dwuwymiarowa tablica (aby utworzyć obiekty w każdym miejscu "siatki"
        Player m_player;

        public Map(string name, int width, int height)
        {
            this.m_name = name;
            this.m_width = width;
            this.m_height = height;

            // init matrix
            initializeFieldsMatrix();

            this.m_player = new Player(5, 5);
        }
        private void initializeFieldsMatrix()
        {
            m_fieldMatrix = new Field[m_width, m_height];
            for (int col = 0; col < m_width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    //dostęp do itego elementy macierzy
                    m_fieldMatrix[col, row] = new FloorField();
                }
            }
        }

        // properties
        public string name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        public int width
        {
            get
            {
                return m_width;
            }
            set
            {
                m_width = value;
            }
        }
        public int height
        {
            get
            {
                return m_height;
            }
            set
            {
                m_height = value;
            }
        }

        // methods
        public Field getField(int x, int y)
        {
            // //(x + y) % 3 == 0 ? Field.Type.FIELD_TYPE_WALL :
            //return new Field(Field.Type.FIELD_TYPE_FLOOR); <--to bylo wczesniej
            //getField ma zwracac element o wspolrzednych xy
            return m_fieldMatrix[x, y];
        }

        public void setField(int x, int y, Field field)
        {
            m_fieldMatrix[x, y] = field;
        }

        public Player player
        {
            get
            {
                return m_player;
            }
            set
            {
                m_player = value;
            }
        }
    }
}
