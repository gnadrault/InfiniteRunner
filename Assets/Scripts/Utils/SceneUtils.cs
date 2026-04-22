using UnityEngine.SceneManagement;

namespace Utils
{
    /// <summary>
    /// Utility class to manage scenes
    /// </summary>
    public static class SceneUtils
    {
        public static void UnloadIfLoaded(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            UnloadIfLoaded(scene);
        }
        
        public static void UnloadIfLoaded(Scene scene)
        {
            if (scene.isLoaded)
                SceneManager.UnloadSceneAsync(scene);
        }
    }
}