using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Load the music of the current scene
    /// </summary>
    public class SceneMusic : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioClip sceneMusic;

        private void Start()
        {
            AudioManager.Instance.PlayMusic(sceneMusic);
        }
        
    }
}