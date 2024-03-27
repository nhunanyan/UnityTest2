using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FactoryPattern.LeaderBoard;
using FactoryPattern.Popups;
using FactoryPattern.SelectModeScripts;
using Test;

namespace FactoryPattern.Menu
{
    public class MenuView : AbstractView<MenuController, MenuModel>
    {
        [SerializeField] private Button leaderBoardButton;
        [SerializeField] private Button selectModeButton;
        [SerializeField] private Button popupButton;
        [SerializeField] private RectTransform container;

        private void Start()
        {
            leaderBoardButton.onClick.AddListener(OnLeaderBoardClicked);
            selectModeButton.onClick.AddListener(OnSelectModeClicked);
            popupButton.onClick.AddListener(OnPopupClicked);
        }

        private void OnPopupClicked()
        {
            var model = new PopupModel
            {
                Title = "Valod",
                Message = "Kalod"
            };
            var popupController = UIManager.Instance.Show<PopupView, PopupController, PopupModel>(true, model);
            popupController.OnResult += result =>
            {

            };
        }

        private void OnLeaderBoardClicked()
        {
            // UIManager.Show<LeaderBoardView, LeaderBoardController, LeaderBoardModel>();
            UIManager.Instance.Show<LeaderBoardView, LeaderBoardController, LeaderBoardModel>();
        }

        private void OnSelectModeClicked()
        {
            UIManager.Instance.Show<SelectModeView, SelectModeController, SelectModeModel>();
        }

        internal override void Initialize()
        {
            
        }
    }
}
