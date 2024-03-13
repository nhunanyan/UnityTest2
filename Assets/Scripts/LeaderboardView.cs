using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DefaultNamespace
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button sortButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private LeaderboardEntryItem entryItemTemplatePrefab;

        private List<LeaderboardEntryItem> _items = new List<LeaderboardEntryItem>();

        private bool _areItemsInitialized;
        private LeaderboardModel _model;

        private void Start()
        {
            sortButton.onClick.AddListener(OnSortClicked);
            nextButton.onClick.AddListener(OnNextClicked);
            backButton.onClick.AddListener(OnBackClicked);
        }

        public void SetModel(LeaderboardModel model)
        {
            _model = model;
            _model.OnLeaderboardLoaded += OnLeaderboardLoaded;
        }

        private void OnLeaderboardLoaded()
        {
            if (!_areItemsInitialized)
            {
                PopulateItems();
                _areItemsInitialized = true;
                return;
            }

            RefreshItems();
        }

        private void RefreshItems()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var entry = _model.LeaderboardInfo.Leaderboard[i];
                var item = _items[i];
                item.Setup(entry);
            }
        }

        private void PopulateItems()
        {
            foreach (var entry in _model.LeaderboardInfo.Leaderboard)
            {
                var item = Instantiate(entryItemTemplatePrefab, container);
                item.Setup(entry);
                _items.Add(item);
            }
        }

        private void OnSortClicked()
        {
            _model.ChangeSortingOrder();
            _model.Load();
        }
        
        private void OnBackClicked()
        {
            _model.ChangeBackPage();
            _model.Load();
        }
        
        private void OnNextClicked()
        {
            _model.ChangeNextPage();
            _model.Load();
            // if (_model.LeaderboardInfo.Leaderboard.Length < 5)
            // {
            //     nextButton.interactable = false;
            // }
        }
    }
}