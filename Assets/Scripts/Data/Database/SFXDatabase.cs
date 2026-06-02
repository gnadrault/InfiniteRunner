using System.Linq;
using UnityEngine;

namespace Data.Database
{
    /// <summary>
    /// SFX Database
    /// </summary>
    [CreateAssetMenu(fileName = "SfxDatabase", menuName = "SyntaxError/Audio/SfxDatabase")]
    public class SfxDatabase : ScriptableObject
    {
        [System.Serializable]
        public class SfxEntry
        {
            public SfxType type;
            public AudioClip clip;
            [Range(0f, 1f)] public float volume = 1f;
        }

        [SerializeField] private SfxEntry[] sounds;

        public SfxEntry Get(SfxType type) => sounds.FirstOrDefault(x => x.type == type);
    }
    
    public enum SfxType
    {
        LetterCollect,
        BonusCollect,
        VirusAttach,
        ShieldBreak,
        Magnet,
        BonusActivate,
        MalusActivate,
        GameOver,
        BestHighScore,
        AlertRedVoice,
        AlertBlueVoice,
        AlertYellowVoice,
        AlertGreenVoice,
    }

}