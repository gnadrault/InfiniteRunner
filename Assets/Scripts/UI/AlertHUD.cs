using System;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AlertHUD : GameBehavior
    {
        public static AlertHUD Instance;
        
        public enum PanelType
        {
            Virus,
            Bonus,
            Malus
        }

        [Serializable]
        private class StatusPanel
        {
            public PanelType type;
            public GameObject panel;
            public TextMeshProUGUI textLabel;
            public TextMeshProUGUI actionLabel;
            [HideInInspector] public float remainingTime;
        }

        [SerializeField] private StatusPanel[] panels;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void ShowPanelTimed(PanelType type, string textLabel, float duration)
        {
            foreach (var panel in panels)
            {
                bool isActive = panel.type == type;
                panel.panel.SetActive(isActive);
                if (isActive)
                {
                    panel.textLabel.text = textLabel;
                    panel.remainingTime = duration;
                }
            }
        }

        public void ShowPanelText(PanelType type, string textLabel, string customText)
        {
            foreach (var panel in panels)
            {
                bool isActive = panel.type == type;
                panel.panel.SetActive(isActive);
                if (isActive)
                {
                    panel.textLabel.text = textLabel;
                    panel.actionLabel.text = customText;
                    panel.remainingTime = -1;
                }
            }
        }

        public void UpdatePanelText(string customText)
        {
            foreach (var panel in panels)
            {
                if (!panel.panel.activeSelf) continue;
                panel.actionLabel.text = customText;
            }
        }

        public void ForceHidePanels()
        {
            foreach (var panel in panels)
            {
                panel.panel.SetActive(false);
            }
        }

        protected override void GameplayUpdate()
        {
            foreach (var panel in panels)
            {
                if (!panel.panel.activeSelf || panel.remainingTime < 0) continue;

                panel.remainingTime -= Time.unscaledDeltaTime;
                panel.actionLabel.text = GetTimer(panel.remainingTime);
                
                if (panel.remainingTime <= 0f)
                {
                    panel.panel.SetActive(false);
                    panel.textLabel.text = string.Empty;
                }
            }
        }

        private string GetTimer(float currentTimer)
        {
            int seconds = Mathf.FloorToInt(currentTimer);
            int milliseconds = Mathf.FloorToInt((currentTimer - seconds) * 100);
            return $"{seconds:D2}:{milliseconds:D2}";
        }
    }
}