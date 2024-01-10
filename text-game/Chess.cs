namespace chess;

internal class Chess
{
    static List<Tile> TileList = new();
    static int BoardSize = 8;

    static void Main()
    {
        BuildBoard(false);
        MovePawn("e4");
    }

    static void BuildBoard(bool rebuild)
    {
        Console.Write("  ");
        for (char letter = 'A'; letter < 'A' + BoardSize; letter++) Console.Write($"  {letter} ");
        Console.WriteLine();

        int index = 0;
        for (var row = BoardSize; row > 0; row--)
        {
            Console.Write($" {row} ");

            for (var column = 1; column <= BoardSize; column++)
            {
                Console.BackgroundColor = (row + column) % 2 != 0 ? ConsoleColor.White : ConsoleColor.DarkGray;

                string tileID = " " + Convert.ToChar('a' + column - 1) + row.ToString() + " ";

                if (rebuild == false)
                {
                    Tile tile = new(tileID, GetPieces(row, column));
                    TileList.Add(tile);
                    Console.Write(tile.Piece);
                }
                else
                {
                    Tile tile = TileList[index++];
                    Console.Write(tile.Piece);
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
        Console.WriteLine("\n\n");
    }


    static string GetPieces(int row, int column)
    {
        if (row == 1 || row == 8)
        {
            switch (column)
            {
                case 1:
                case 8:
                    return " R  ";
                case 2:
                case 7:
                    return " H  ";
                case 3:
                case 6:
                    return " B  ";

                case 4:
                    return " Q  ";
                case 5:
                    return " K  ";
            }
        }
        else if (row == 2 || row == 7)
        {
            return " P  ";
        }
        return "    ";
    }


    static void MovePawn(string notation)
    {
        foreach (var tile in TileList)
        {
            tile.Piece = " P  ";
        }

        BuildBoard(true);
    }
}
