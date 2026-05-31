using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Elements.Collectibles
{
    public class Pixel : GameBehavior
    {
        [SerializeField] private Renderer pixelRenderer;
        
        private enum PixelColor { Red, Green, Blue }
    
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private MaterialPropertyBlock _propBlock;

        private void Awake()
        {
            PixelColor randomColor = (PixelColor)Random.Range(0, 3);
            SetColor(randomColor);
        }

        private void SetColor(PixelColor color)
        {
            if (_propBlock == null) _propBlock = new MaterialPropertyBlock();
    
            Color baseColor = color switch
            {
                PixelColor.Red   => new Color(1f, 0f, 0f),
                PixelColor.Green => new Color(0f, 1f, 0f),
                PixelColor.Blue  => new Color(0f, 0.4f, 1f),
                _                => Color.white
            };
    
            pixelRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor(BaseColor, baseColor);
            _propBlock.SetColor(EmissionColor, baseColor * 4f);
            pixelRenderer.SetPropertyBlock(_propBlock);
        }
    }
}