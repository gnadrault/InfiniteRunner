using Player;
using UnityEngine;
using Utils;

namespace Gameplay.Elements.Obstacles
{
    public class Obstacle : Element
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        
        private Renderer[] _renderers;
        private MaterialPropertyBlock _matPropertyBlock;
        
        private void Awake()
        {
            _renderers = gameObject.GetComponentsInChildren<Renderer>();
            _matPropertyBlock =  new MaterialPropertyBlock();
        }
        
        public override void OnPlayerCollision(Transform position)
        {
            if (PlayerController.Instance.HasGhost())
                GameEvents.OnGhostBroken?.Invoke();
            else
                PlayerController.Instance.Die();
        }
        
        public void OnTransparencyCollision()
        {
            Colors.SetTransparency(_renderers, _matPropertyBlock, BaseColor, 0.3f);
        }
    }
}
