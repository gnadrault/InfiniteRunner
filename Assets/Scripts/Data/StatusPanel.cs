using System;
using TMPro;
using UnityEngine;

namespace Data
{
    public enum AlertPanelType
    {
        Virus,
        Bonus,
        Malus
    }
    
    [Serializable]
    public class AlertStatusPanel
    {
        public AlertPanelType type;
        public GameObject panelContainer;
        public TextMeshProUGUI textLabel;
        public TextMeshProUGUI actionLabel;
    }
}