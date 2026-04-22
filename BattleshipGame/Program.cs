using System;
using Battleship.Models;
using Battleship.Enum;
using Battleship.Interfaces;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== BATTLESHIP GAME ===\n");

        Console.Write("Enter Player 1 name: ");
        var p1 = new Player(Console.ReadLine()!);

        Console.Write("Enter Player 2 name: ");
        var p2 = new Player(Console.ReadLine()!);

        var game = new GameController(p1, p2);

        SetupPhase(game, p1);
        SetupPhase(game, p2);

        game.StartGame();

        while (game.GetStatus() != GameStatus.End)
        {
            Console.Clear();
            var current = game.GetCurrentPlayer();
            Console.WriteLine($"{current.Name}'s turn\n");

            PrintBoard(game.GetBoard(game.GetOpponent()), true);

            Console.Write("Enter attack (x y): ");
            var input = Console.ReadLine()!.Split(' ');

            int x = int.Parse(input[0]);
            int y = int.Parse(input[1]);

            bool success = game.MakeMove(new Position(x, y));

            if (!success)
            {
                Console.WriteLine("Invalid move!");
                Console.ReadKey();
            }
        }

        Console.WriteLine($"Winner: {game.GetWinner()!.Name}");
    }

    static void SetupPhase(GameController game, Player player)
    {
        Console.Clear();
        Console.WriteLine($"=== {player.Name} Place Ships ===\n");

        foreach (ShipType type in Enum.GetValues(typeof(ShipType)))
        {
            bool placed = false;

            while (!placed)
            {
                Console.WriteLine($"Placing {type}");
                PrintBoard(game.GetBoard(player), false);

                Console.Write("Enter position (x y): ");
                var posInput = Console.ReadLine()!.Split(' ');
                int x = int.Parse(posInput[0]);
                int y = int.Parse(posInput[1]);

                Console.Write("Orientation (H/V): ");
                var ori = Console.ReadLine()!.ToUpper();

                var orientation = ori == "H" ? Orientation.Horizontal : Orientation.Vertical;

                placed = game.PlaceShip(player, type, new Position(x, y), orientation);

                if (!placed)
                {
                    Console.WriteLine("Invalid placement! Try again.");
                }
            }
        }
    }

    static void PrintBoard(IBoard board, bool hideShips)
    {
        for (int y = 0; y < board.Size; y++)
        {
            for (int x = 0; x < board.Size; x++)
            {
                var cell = board.GetCell(new Position(x, y));

                char c = '.';

                if (cell.State == CellState.Hit) c = 'X';
                else if (cell.State == CellState.Miss) c = 'O';
                else if (!hideShips && cell.Ship != null) c = 'S';

                Console.Write(c + " ");
            }
            Console.WriteLine();
        }
    }
}
