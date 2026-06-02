using UnityEngine;

namespace Data.Database
{
    /// <summary>
    /// Game feel profiles to apply for virus, word effects
    /// </summary>
    [CreateAssetMenu(fileName = "GameFeelProfile", menuName = "SyntaxError/GameFeelProfile")]
    public class GameFeelProfile  : ScriptableObject
    {
        [Header("References")]
        [SerializeField] private VignetteSection vignette;
        [SerializeField] private ScoreSection score;
        [SerializeField] private ObscureScreenSection obscureScreen;
        [SerializeField] private StroboscopeSection stroboscope;

        public VignetteSection Vignette => vignette;
        public ScoreSection Score => score;
        public ObscureScreenSection ObscureScreen => obscureScreen;
        public StroboscopeSection Stroboscope => stroboscope;
    }
    
    [System.Serializable]
    public class VignetteSection
    {
        public bool enabled;
        public Color color = Color.red;
        [Range(0f, 1f)] public float intensity = 0.3f;
        [Range(0f, 1f)] public float smoothness = 0.2f;
        public bool pulse;
        public float pulseSpeed = 2f;
    }

    [System.Serializable]
    public class ScoreSection
    {
        public bool enabled;
        public float shakeIntensity = 3f;
        public Color color = Color.red;
    }
    
    [System.Serializable]
    public class ObscureScreenSection
    {
        public bool enabled;
        public Texture dirtTexture;
        public float dirtIntensity = 165f;
    }
    
    [System.Serializable]
    public class StroboscopeSection
    {
        public bool enabled;
        public float invisibleTime = 0.6f;
        public float visibleTime;
    }
}