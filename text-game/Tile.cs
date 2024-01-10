namespace chess
{
    internal class Tile
    {
        public string TileID;
        public bool Empty = true;

        public Tile(string tileID) 
        {
            TileID = tileID;
        }
    }
}
