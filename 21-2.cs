/*
int playerOneStart = int.Parse(Console.ReadLine().Split()[^1]);
int playerTwoStart = int.Parse(Console.ReadLine().Split()[^1]);

Universe.playerOneStart = playerOneStart;
Universe.playerTwoStart = playerTwoStart;

Universe ඞ = new();

Console.WriteLine(Universe.playerOneWins);
Console.WriteLine(Universe.playerTwoWins);

class Universe
{
	public long occurences = 1;

	public int playerOneScore, playerTwoScore;
	public int playerOnePosition, playerTwoPosition;

	public static int playerOneStart, playerTwoStart;
	public static long playerOneWins = 0, playerTwoWins = 0;

	public Universe()
	{
		playerOneScore = 0;
		playerTwoScore = 0;
		playerOnePosition = playerOneStart;
		playerTwoPosition = playerTwoStart;
		
		Roll();
	}

	public Universe(Universe x, int roll, bool playerOne)
	{
		playerOneScore = x.playerOneScore;
		playerOnePosition = x.playerOnePosition;
		
		playerTwoScore = x.playerTwoScore;
		playerTwoPosition = x.playerTwoPosition;
		occurences = x.occurences;

		if (playerOne)
		{
			playerOnePosition += roll;
			occurences *= Frequency(roll);
			if (playerOnePosition > 10) playerOnePosition -= 10;
			playerOneScore += playerOnePosition;

			if (playerOneScore >= 21)
			{
				playerOneWins += occurences;
				return;
			}
		}
		else
		{
			playerTwoPosition += roll;
			occurences *= Frequency(roll);
			if (playerTwoPosition > 10) playerTwoPosition -= 10;
			playerTwoScore += playerTwoPosition;

			if (playerTwoScore >= 21)
			{
				playerTwoWins += occurences;
				return;
			}
		}

		Roll(!playerOne);
	}

	public void Roll(bool playerOne = true)
	{
		for (int i = 3; i <= 9; i++)
		{
			new Universe(this, i, playerOne);
		}
	}

	public static int Frequency(int x) => x switch
	{
		3 => 1,
		4 => 3,
		5 => 6,
		6 => 7,
		7 => 6,
		8 => 3,
		9 => 1,
		_ => throw new Exception("sus")
	};
}
*/