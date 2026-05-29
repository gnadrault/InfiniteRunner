using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Manage Game UI
    /// </summary>
    public class UIFunction : MonoBehaviour
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

        /*public void Resume()
        {
            GameManager.Instance.SetGameState(GameState.Gameplay);
        }
        
        public void NewGame()
        {
            GameManager.Instance.SetGameState(GameState.Gameplay);
            GameManager.Instance.speedFactor *= 1.2f;
            StageManager.Instance.Reset();
        }*/
    }
}
