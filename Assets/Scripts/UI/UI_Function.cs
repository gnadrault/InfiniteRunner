using Audio;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIFunction : GameBehavior
    {

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
