namespace chess;

internal class Chess
{
    static List<Tile> TileList;
    static int BoardSize = 8;

    static void Main() => CreateTiles();

    static void CreateTiles() 
    {
        Console.Write("  ");
        for (char letter = 'A'; letter < 'A' + BoardSize; letter++) Console.Write($"  {letter} ");
        Console.WriteLine();

        for (var row = BoardSize; row > 0; row--) 
        {
            Console.Write($" {row} ");

            for (var column = 0; column < BoardSize; column++)
            {
                Console.BackgroundColor = (row + column) % 2 == 0 ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.Write($" {Convert.ToChar('A' + column)}{row} ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
    }
}
