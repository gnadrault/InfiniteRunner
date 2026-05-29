using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject scoresMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject creditsMenu;

        public void DisplayScoresMenu()
        {
            scoresMenu.SetActive(true);
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }

        public void DisplayOptionsMenu()
        {
            scoresMenu.SetActive(false);
            optionsMenu.SetActive(true);
            creditsMenu.SetActive(false);
        }
        
        public void DisplayCreditsMenu()
        {
            scoresMenu.SetActive(false);
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
        
    }
}