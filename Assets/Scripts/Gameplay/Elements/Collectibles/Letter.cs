using Player;
using TMPro;
using UnityEngine;

namespace Gameplay.Elements.Collectibles
{
    public class Letter: Collectible
    {
        [SerializeField] private TextMeshPro label;
        
        public string Label => label.text;

        public void SetLabelText(string text)
        {
            label.text = text;
        }

        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            player.CollectLetter(this);
            Destroy(gameObject);
        }
    }
}