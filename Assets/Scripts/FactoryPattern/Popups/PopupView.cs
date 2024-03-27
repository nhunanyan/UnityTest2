using System;
using System.Collections.Generic;
using Test;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryPattern.Popups
{
    public class PopupView : AbstractView<PopupController, PopupModel>
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private TextMeshProUGUI titleContent;
        [SerializeField] private TextMeshProUGUI content;

        private void Start()
        {
            closeButton.onClick.AddListener(OnClose);
            yesButton.onClick.AddListener(() => Controller.OnYes());
            noButton.onClick.AddListener(() => Controller.OnNo());
        }

        private void OnClose()
        {
            UIManager.Instance.Close(Controller, true);
        }

        internal override void Initialize()
        {
            titleContent.text = Model.Title;
            content.text = Model.Message;
        }
    }
}