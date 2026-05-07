using Player;
using TMPro;
using UnityEngine;

namespace World.GameElement.Collectible
{
    public class Letter: CollectibleElement
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