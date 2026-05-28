using UnityEngine;
using UnityEngine.Rendering;

namespace Feedback.Data
{
    [CreateAssetMenu(fileName = "GameFeelProfile", menuName = "SyntaxError/GameFeelProfile")]
    public class GameFeelProfile  : ScriptableObject
    {
        [SerializeField] private VignetteSection vignette;
        [SerializeField] private ScoreSection score;
        [SerializeField] private BorderSection border;
        [SerializeField] private GlowSection glow;
        
        // TODO Screen shake

        public VignetteSection Vignette => vignette;
        public ScoreSection Score => score;
        public BorderSection Border => border;
        public GlowSection Glow => glow;
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
        public bool shake;
        public float shakeIntensity = 3f;
        public bool colorOverride;
        public Color color = Color.red;
    }

    [System.Serializable]
    public class BorderSection
    {
        public bool enabled;
        public Color color = Color.red;
        public bool pulse;
        public float pulseSpeed = 2f;
    }

    [System.Serializable]
    public class GlowSection
    {
        public bool enabled;
        public Color color = Color.green;
        [Range(0f, 1f)] public float intensity = 0.5f;
    }
}