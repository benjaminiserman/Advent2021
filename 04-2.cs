int[] moves = (from string x in Console.ReadLine().Split(',') select int.Parse(x)).ToArray();

List<int[][]> boards = new();
int boardSize = 5;

while (true)
{
    string input = Console.ReadLine();
    if (input == "end") break;

    int[][] board = new int[boardSize][];
    for (int i = 0; i < boardSize; i++)
    {
        board[i] = (from string x in Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries) select int.Parse(x)).ToArray();
    }

    boards.Add(board);
}

List<int> drawn = new();
List<int[][]> winners = new();

while (true)
{
    try
    {
        drawn.Add(moves[drawn.Count]);
    }
    catch
    {
        Console.WriteLine($"winners: {winners.Count}/{boards.Count}");
        throw;
    }
    Console.WriteLine(drawn[^1]);

    if (winners.Count == boards.Count - 1) break;

    foreach (var board in boards)
    {
        if (winners.Contains(board)) continue;

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (!drawn.Contains(board[x][y]))
                {
                    goto NextRow;
                }
            }

            winners.Add(board);
            goto End;
            NextRow:;
        }

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                if (!drawn.Contains(board[x][y]))
                {
                    goto NextRow;
                }
            }

            winners.Add(board);
            goto End;
            NextRow:;
        }

        End:;
    }
}

Console.Write("Moves:");
foreach (var d in drawn) Console.Write($" {d}");
Console.WriteLine();

Console.WriteLine("Wining board found:");
int[][] found = boards.First(x => !winners.Contains(x));
int sum = 0;
foreach (var x in found)
{
    foreach (var y in x)
    {
        Console.Write($"{y} ");
        if (!drawn.Contains(y)) sum += y;
    }

    Console.WriteLine();
}

Console.WriteLine(sum * drawn[^1]);