using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Utility class for colors
    /// </summary>
    public static class Colors
    {
        public static readonly Color HighlightBonus = new(0f, 216f / 255f, 1f);
        public static readonly Color HighlightMalus = new(204f / 255f, 0f, 0f);
        public static readonly Color Default = Color.white;
        
        public static void SetTransparency(Renderer[] renderers, MaterialPropertyBlock matPropBlock, int baseColor, float alpha)
        {
            foreach (var r in renderers)
            {
                r.GetPropertyBlock(matPropBlock);
                Color c = matPropBlock.GetColor(baseColor);
                c.a = alpha;
                matPropBlock.SetColor(baseColor, c);
                r.SetPropertyBlock(matPropBlock);
            }

        }
    }
}