using Core;
using Data;
using UnityEngine;

namespace UI
{
    public class AlertPanelUI : GameBehavior
    {
        public static AlertPanelUI Instance;
        
        [SerializeField] private AlertStatusPanel[] alertPanels;
        
        private AlertStatusPanel _panelDisplayed;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void ShowPanel(AlertPanelType type, string textLabel, string action)
        {
            foreach (var alertPanel in alertPanels)
            {
                if (alertPanel.type == type)
                {
                    alertPanel.textLabel.text = textLabel;
                    alertPanel.actionLabel.text = action;
                    alertPanel.panelContainer.SetActive(true);
                    _panelDisplayed = alertPanel;
                }
                else
                {
                    alertPanel.panelContainer.SetActive(false);
                }
            }
        }

        public void SetActionText(string customText)
        {
            if (_panelDisplayed == null) return;
            _panelDisplayed.actionLabel.text = customText;
        }

        public void HideActivePanel()
        {
            if (_panelDisplayed == null) return;
            _panelDisplayed.panelContainer.SetActive(false);
            _panelDisplayed = null;
        }
    }
}