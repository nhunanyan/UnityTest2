using UnityEngine;
using System.Collections.Generic;
using FactoryPattern.Menu;
using Test;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace FactoryPattern.SelectModeScripts
{
    public class SelectModeView : AbstractView<SelectModeController, SelectModeModel>
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button endButton;
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private SelectModeEntryItem entryItemTemplatePrefab;
        [SerializeField] private ScrollRect scrollRect;
        
        private List<SelectModeEntryItem> _items = new List<SelectModeEntryItem>();

        private bool _areItemsInitialized;

        internal override void Initialize()
        {
            Model.OnSelectModeLoaded += OnSelectModeLoaded;
        }

        private void Start()
        {
            endButton.onClick.AddListener(OnEndClicked);
            startButton.onClick.AddListener(OnStartClicked);
            closeButton.onClick.AddListener(OnCloseClicked);
        }

        private void OnCloseClicked()
        {
            Controller.Hide();
            UIManager.Instance.Show<MenuView, MenuController, MenuModel>();
        }


        private void OnSelectModeLoaded()
        {
            endButton.interactable = true;
            startButton.interactable = false;
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
                var entry = Model.SelectModeInfo.SelectMode[i];
                var item = _items[i];
                item.Setup(entry);
            }
        }

        private void PopulateItems()
        {
            foreach (var entry in Model.SelectModeInfo.SelectMode)
            {
                var item = Instantiate(entryItemTemplatePrefab, container);
                item.Setup(entry);
                _items.Add(item);
            }
        }
        
        private void OnStartClicked()
        {
            SetNavigationState();
            scrollRect.horizontalNormalizedPosition = 0;
        }

        private void OnEndClicked()
        {
            SetNavigationState();
            scrollRect.horizontalNormalizedPosition = 1;
        }

        private void SetNavigationState()
        {
            endButton.interactable = !endButton.interactable;
            startButton.interactable = !startButton.interactable;
        }
        
    }
}