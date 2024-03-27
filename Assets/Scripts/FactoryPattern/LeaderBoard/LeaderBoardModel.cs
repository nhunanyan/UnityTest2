using System;

namespace FactoryPattern.LeaderBoard
{
    public class LeaderBoardModel : IModel
    {
        public LeaderboardResult LeaderboardResult { get; set; }
        public event Action OnLeaderboardLoaded;

        public void Load(int page, int count, bool descending)
        {
            LeaderboardResult = LeaderboardDataLoader.Load(page, count, descending);
            OnLeaderboardLoaded?.Invoke();
        }

        public void Dispose()
        {
            foreach (Delegate @delegate in OnLeaderboardLoaded.GetInvocationList())
            {
                OnLeaderboardLoaded -= (Action)@delegate;
            }
        }
    }
}