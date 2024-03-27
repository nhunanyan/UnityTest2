using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace FactoryPattern.LeaderBoard
{
    public static class LeaderboardDataLoader
    {
        private static int _count;

        public static LeaderboardResult Load(int page, int count, bool descending)
        {
            var json = Resources.Load<TextAsset>("Data/leaderboard").text;
            var entries = JsonConvert.DeserializeObject<LeaderboardEntryInfo[]>(json);
            var result =
                descending
                    ? entries.OrderByDescending(info => info.Score)
                    : entries.OrderBy(info => info.Score)
                        .AsEnumerable();
            result = result.Skip(page * count).Take(count);

            return new LeaderboardResult(result.ToArray(), entries.Length);
        }
    }
}