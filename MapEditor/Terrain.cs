namespace MapEditor
{

    class Terrain : Tile
    {
        public TileTypes Type { get; set; }

        public Terrain(int x, int y, TileTypes type)
            : base(x, y)
        {
            Type = type;
        }
    }

}
