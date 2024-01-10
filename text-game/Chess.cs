namespace chess;

internal class Chess
{
    static string TileList = BuildBoard();

    static void Main() => BuildBoard();

    static void BuildBoard() 
    {
        for (var i = 0; i < 8; i++) 
        {
            Console.Write("");
        }
    }
}
