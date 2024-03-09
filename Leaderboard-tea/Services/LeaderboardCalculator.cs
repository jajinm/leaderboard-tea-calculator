using Leaderboard.Models;

/**
 * REQUERIMENTO:
 *
 * A função CalculaPlaces deve ser implementada.
 * A função distribui as vagas dos usuários, levando em consideração as restrições para obtenção dos primeiros lugares e os pontos conquistados pelos usuários.
 * Veja especificações técnicas detalhadas em readme.txt
 */

 namespace Leaderboard.Services
{
    public class LeaderboardCalculator : ILeaderboardCalculator
    {
        public IEnumerable<UserWithPlace> CalculatePlaces(IEnumerable<IUserWithScore> usersWithScores, LeaderboardMinScores leaderboardMinScores)
        {
            // TODO: implement code here
            int totalTransactions = 1;
            int calculator = 3;
            int place = 0;

            return
                (usersWithScores ?? throw new ArgumentNullException(nameof(usersWithScores)))
                .OrderByDescending(s => s.Score)
                .Select(userWithPlace =>
                {
                    var currentTransactionId = totalTransactions;

                    if (userWithPlace.Score >= leaderboardMinScores.FirstPlaceMinScore && currentTransactionId < 4)
                    {
                        place += 1;
                        calculator -= 1;
                    }
                    else if (userWithPlace.Score >= leaderboardMinScores.SecondPlaceMinScore && currentTransactionId < 4)
                    {
                        currentTransactionId = 2;
                        calculator -= 1;
                        if (place == 0)
                            place += 2;
                        else place += 1;
                    }
                    else if (userWithPlace.Score >= leaderboardMinScores.ThirdPlaceMinScore && currentTransactionId < 4)
                    {
                        currentTransactionId = 3;
                        calculator -= 1;
                        if (place == 0)
                            place += 3;
                        else if (place == 1)
                            place += 2;
                        else place += 1;
                    }
                    else
                        currentTransactionId = calculator + totalTransactions;

                    totalTransactions++;

                    return new UserWithPlace(userId: userWithPlace.UserId, place: currentTransactionId);
                });
        }
    }
}