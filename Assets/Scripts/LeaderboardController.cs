using UnityEngine;

namespace DefaultNamespace
{
    public class LeaderboardController : MonoBehaviour
    {
        [SerializeField] private LeaderboardView view;

        private readonly LeaderboardModel _model = new(0, 5, true);

        private void Start()
        {
            view.SetModel(_model);
            _model.Load();
        }
    }
}