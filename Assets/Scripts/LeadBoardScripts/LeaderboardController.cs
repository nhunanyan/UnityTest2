using UnityEngine;

namespace DefaultNamespace
{
    public class LeaderboardController : MonoBehaviour
    {
        public const string UserName = "Nina";
        [SerializeField] private LeaderboardView view;

        private readonly LeaderboardModel _model = new();

        public bool HasNext => (Page + 1) * Count <= _model.LeaderboardResult.Total;

        [field: SerializeField] public int Page { get; private set; } = 0;
        [field: SerializeField] public int Count { get; private set; } = 5;
        [field: SerializeField] public bool Descending { get; private set; }

        private void Start()
        {
            view.SetModel(_model);
            Load();
        }

        private void Load()
        {
            _model.Load(Page, Count, Descending);
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