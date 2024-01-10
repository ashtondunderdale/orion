namespace chess
{
    internal class Tile
    {
        public string TileID;
        public string Piece = "";


        public Tile(string tileID, string piece) 
        {
            TileID = tileID;
            Piece = piece;
        }
    }
}
