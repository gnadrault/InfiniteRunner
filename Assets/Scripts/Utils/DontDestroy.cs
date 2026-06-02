using Core;

namespace Utils
{
    /// <summary>
    /// Utility script to prevent game object to be destroyed
    /// </summary>
    public class DontDestroy : GameBehavior
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
