using Core;

namespace Utils
{
    public class DontDestroy : GameBehavior
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
