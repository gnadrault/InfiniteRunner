using Core;
using UnityEngine;

namespace Audio
{
    public class AudioManager : GameBehavior
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
