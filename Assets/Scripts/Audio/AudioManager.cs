using TMPro;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI musicValue;
        [SerializeField] private TextMeshProUGUI sfxValue;
        
        public static AudioManager Instance;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void OnMusicChanged(System.Single value)
        {
            print(value);
            musicValue.text = value.ToString();
        }
        
        public void OnSfxChanged(System.Single value)
        {
            sfxValue.text = value.ToString();
        }
    }
}
