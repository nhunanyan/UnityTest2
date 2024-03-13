using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace DefaultNamespace
{
    public static class LeaderboardDataLoader
    {
        private static int _count;
        public static LeaderboardInfo Load(int page, int count, bool descending)
        {
            var json = Resources.Load<TextAsset>("Data/leaderboard").text;
            var leaderboard = JsonConvert.DeserializeObject<LeaderboardInfo>(json);
            leaderboard.Leaderboard = descending
                ? leaderboard.Leaderboard.OrderByDescending(info => info.Score).ToArray()
                : leaderboard.Leaderboard.OrderBy(info => info.Score).ToArray();
            if (page * count + count < leaderboard.Leaderboard.GetLength(0))
            {
                leaderboard.Leaderboard = leaderboard.Leaderboard.Skip(page * count).Take(count).ToArray();
            }
            else
            {
                _count = leaderboard.Leaderboard.GetLength(0) - (page * count);
                leaderboard.Leaderboard = leaderboard.Leaderboard.Skip(page * count).Take(_count).ToArray();
            }
            
            return leaderboard;
        }
    }
}