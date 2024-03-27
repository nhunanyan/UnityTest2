using System;
using System.Collections.Generic;
using FactoryPattern.Menu;
using Test;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryPattern.LeaderBoard
{
    public class LeaderBoardView : AbstractView<LeaderBoardController, LeaderBoardModel>
    {
        
        [SerializeField] private Button closeButton;
        [SerializeField] private Button sortButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private LeaderboardEntryItem entryItemTemplatePrefab;
        [SerializeField] private string highlightUserName;

        private List<LeaderboardEntryItem> _items = new List<LeaderboardEntryItem>();

        private bool _areItemsInitialized;
        internal override void Initialize()
        {
            Model.OnLeaderboardLoaded += OnLeaderboardLoaded;
        }

        private void Start()
        {
            sortButton.onClick.AddListener(OnSortClicked);
            nextButton.onClick.AddListener(OnNextClicked);
            previousButton.onClick.AddListener(OnPreviousClicked);
            closeButton.onClick.AddListener(OnCloseClicked);
        }

        private void OnCloseClicked()
        {
            Controller.Hide();
            UIManager.Instance.Show<MenuView, MenuController, MenuModel>();
        }
        
        private void OnLeaderboardLoaded()
        {
            SetNavigationState();
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
                if (i >= Model.LeaderboardResult.Entries.Length)
                {
                    _items[i].gameObject.SetActive(false);
                    continue;
                }

                _items[i].gameObject.SetActive(true);
                var entry = Model.LeaderboardResult.Entries[i];
                var item = _items[i];
                var position = Controller.Page * Controller.Count + i;
                item.Setup(position, entry);
                item.Highlight(highlightUserName);
            }
        }

        private void PopulateItems()
        {
            _items.Clear();
            for (int i = 0; i < Controller.Count; i++)
            {
                var item = Instantiate(entryItemTemplatePrefab, container);
                var entry = Model.LeaderboardResult.Entries[i];
                var position = Controller.Page * Controller.Count + i;
                item.Setup(position, entry);
                item.Highlight(highlightUserName);
                _items.Add(item);
            }
        }

        private void OnSortClicked()
        {
            Controller.Sort();
        }

        private void OnPreviousClicked()
        {
            Controller.PreviousPage();
        }

        private void OnNextClicked()
        {
            Controller.NextPage();
        }

        private void SetNavigationState()
        {
            previousButton.interactable = Controller.Page > 0;
            nextButton.interactable = Controller.HasNext;
        }

        
    }
}