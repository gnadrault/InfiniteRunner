using Core;
using UnityEngine;

namespace UI
{
    
    /// <summary>
    /// Manage menus display in the main menu (scores, options, credits)
    /// </summary>
    public class MenuManager : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private GameObject scoresMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject creditsMenu;
        
        public void DisplayScoresMenu() => ShowOnly(scoresMenu);
        public void DisplayOptionsMenu() => ShowOnly(optionsMenu);
        public void DisplayCreditsMenu() => ShowOnly(creditsMenu);

        private void ShowOnly(GameObject menu)
        {
            scoresMenu.SetActive(menu == scoresMenu);
            optionsMenu.SetActive(menu == optionsMenu);
            creditsMenu.SetActive(menu == creditsMenu);
        }
    }
}