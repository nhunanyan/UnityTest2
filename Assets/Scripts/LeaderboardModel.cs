using System;
using System.Linq;
using Unity.VisualScripting;

namespace DefaultNamespace
{
    public class LeaderboardModel
    {
        private readonly int _count;
        private int _page;
        private bool _descending;
        public LeaderboardInfo LeaderboardInfo { get; private set; }
        public LeaderboardInfo AllLeaderboardInfo { get; private set; }
        public event Action OnLeaderboardLoaded;

        public LeaderboardModel(int page, int count, bool descending)
        {
            _page = page;
            _count = count;
            _descending = descending;
        }

        public void Load()
        {
            LeaderboardInfo = LeaderboardDataLoader.Load(_page, _count, _descending);
            
            OnLeaderboardLoaded?.Invoke();
        }

        public void ChangeSortingOrder()
        {
            _descending = !_descending;
        }

        public void ChangeNextPage()
        {
            _page++;
        }

        public void ChangeBackPage()
        {
            _page--;

            var info = LeaderboardInfo;
            //info.Leaderboard = AllLeaderboardInfo.Leaderboard.Skip(_page * _count).Take(_count).ToArray();
            LeaderboardInfo = info;

        }
    }
}