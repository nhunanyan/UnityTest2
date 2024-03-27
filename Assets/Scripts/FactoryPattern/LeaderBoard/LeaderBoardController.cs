using UnityEngine;

namespace FactoryPattern.LeaderBoard
{
    public class LeaderBoardController : AbstractController<LeaderBoardModel>
    {
        public override void Show()
        {
            Load();
        }

        public override void Hide()
        {
            
        }

        public const string UserName = "Nina";

        public bool HasNext => (Page + 1) * Count <= Model.LeaderboardResult.Total;

        public int Page { get; private set; } = 0;
        public int Count { get; private set; } = 5;
        public bool Descending { get; private set; } = false;
        

        private void Load()
        {
            Model.Load(Page, Count, Descending);
        }

        public void Sort()
        {
            Descending = !Descending;
            Load();
        }

        public void NextPage()
        {
            Page++;
            Load();
        }

        public void PreviousPage()
        {
            Page--;
            if (Page < 0)
            {
                Page = 0;
            }

            Load();
        }
    }
}