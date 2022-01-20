namespace MapEditor
{

    class Decor : Tile
    {
        public DecorTypes Type { get; set; }

        public Decor(int x, int y, DecorTypes type)
            : base(x, y)
        {
            Type = type;
        }
    }
}
