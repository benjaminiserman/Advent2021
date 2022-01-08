int playerOneScore = 0;
int playerTwoScore = 0;

int playerOnePosition = int.Parse(Console.ReadLine().Split()[^1]);
int playerTwoPosition = int.Parse(Console.ReadLine().Split()[^1]);

int currentRoll = 0;
int rollCount = 0;

while (playerOneScore < 1000 & playerTwoScore < 1000)
{
	playerOnePosition += Roll() + Roll() + Roll();
	while (playerOnePosition > 10) playerOnePosition -= 10;
	playerOneScore += playerOnePosition;

	if (playerOneScore > 1000) break;

	playerTwoPosition += Roll() + Roll() + Roll();
	while (playerTwoPosition > 10) playerTwoPosition -= 10;
	playerTwoScore += playerTwoPosition;
}

int losingScore = playerOneScore < playerTwoScore ? playerOneScore : playerTwoScore;

Console.WriteLine(losingScore * rollCount);

int Roll()
{
	currentRoll++;
	rollCount++;
	if (currentRoll > 100) currentRoll = 1;
	return currentRoll;
}