using Data;
using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Obstacles
{
    [System.Serializable]
    public class Obstacle : Element
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        
        [SerializeField] private ObstacleSize size;
        [SerializeField] private ObstacleType type;
        [SerializeField] private bool isMobile;
        
        private Renderer[] _renderers;
        private MaterialPropertyBlock _matPropertyBlock;
        
        private void Awake()
        {
            _renderers = gameObject.GetComponentsInChildren<Renderer>();
            _matPropertyBlock =  new MaterialPropertyBlock();
        }
        
        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            if (player.HasGhost())
                GameEvents.OnGhostBroken?.Invoke();
            else
                player.Die();
        }
        
        public void OnTransparencyCollision()
        {
            Colors.SetTransparency(_renderers, _matPropertyBlock, BaseColor, 0.3f);
        }

        public ObstacleSize Size => size;
        public ObstacleType Type => type;
        public bool IsMobile => isMobile;
    }
}
