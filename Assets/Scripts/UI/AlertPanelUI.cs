using Core;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Manage the Alert Panel message text and timer
    /// </summary>
    public class AlertPanelUI : GameBehavior
    {
        public static AlertPanelUI Instance;
        
        [Header("Settings")]
        [SerializeField] private AlertStatusPanel[] alertPanels;
        
        private AlertStatusPanel _panelDisplayed;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// Display the alert panel depending on the AlertPanelType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="textLabel"></param>
        /// <param name="action"></param>
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