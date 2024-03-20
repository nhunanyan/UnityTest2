using System;

namespace DefaultNamespace
{
    public class LeaderboardModel
    {
        public LeaderboardResult LeaderboardResult { get; private set; }
        public event Action OnLeaderboardLoaded;

        public void Load(int page, int count, bool descending)
        {
            LeaderboardResult = LeaderboardDataLoader.Load(page, count, descending);
            OnLeaderboardLoaded?.Invoke();
        }
    }
}