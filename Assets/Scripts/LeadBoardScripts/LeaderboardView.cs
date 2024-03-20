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
        [SerializeField] private Button previousButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private LeaderboardEntryItem entryItemTemplatePrefab;
        [SerializeField] private string highlightUserName;

        [SerializeField] private LeaderboardController controller;

        private List<LeaderboardEntryItem> _items = new List<LeaderboardEntryItem>();

        private bool _areItemsInitialized;
        private LeaderboardModel _model;

        private void Start()
        {
            sortButton.onClick.AddListener(OnSortClicked);
            nextButton.onClick.AddListener(OnNextClicked);
            previousButton.onClick.AddListener(OnPreviousClicked);
        }

        public void SetModel(LeaderboardModel model)
        {
            _model = model;
            _model.OnLeaderboardLoaded += OnLeaderboardLoaded;
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
                if (i >= _model.LeaderboardResult.Entries.Length)
                {
                    _items[i].gameObject.SetActive(false);
                    continue;
                }

                _items[i].gameObject.SetActive(true);
                var entry = _model.LeaderboardResult.Entries[i];
                var item = _items[i];
                var position = controller.Page * controller.Count + i;
                item.Setup(position, entry);
                item.Highlight(highlightUserName);
            }
        }

        private void PopulateItems()
        {
            _items.Clear();
            for (int i = 0; i < controller.Count; i++)
            {
                var item = Instantiate(entryItemTemplatePrefab, container);
                var entry = _model.LeaderboardResult.Entries[i];
                var position = controller.Page * controller.Count + i;
                item.Setup(position, entry);
                item.Highlight(highlightUserName);
                _items.Add(item);
            }
        }

        private void OnSortClicked()
        {
            controller.Sort();
        }

        private void OnPreviousClicked()
        {
            controller.PreviousPage();
        }

        private void OnNextClicked()
        {
            controller.NextPage();
        }

        private void SetNavigationState()
        {
            previousButton.interactable = controller.Page > 0;
            nextButton.interactable = controller.HasNext;
        }
    }
}