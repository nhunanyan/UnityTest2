namespace FactoryPattern.LeaderBoard
{
    public struct LeaderboardResult
    {
        public LeaderboardEntryInfo[] Entries { get; }
        public int Total { get; }

        public LeaderboardResult(LeaderboardEntryInfo[] entries, int total)
        {
            Entries = entries;
            Total = total;
        }
    }
}