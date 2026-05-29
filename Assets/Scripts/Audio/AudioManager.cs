using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}
