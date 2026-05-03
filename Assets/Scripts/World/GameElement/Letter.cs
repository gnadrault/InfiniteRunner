
using Player;
using TMPro;
using UnityEngine;

namespace World.GameElement
{
    public class Letter: Collectible
    {
        [SerializeField] private TextMeshPro label;

        public void SetLabelText(string text)
        {
            label.text = text;
        }

        public override void OnPlayerCollision(PlayerController player)
        {
            player.Collect(this);
            Destroy(gameObject);
        }
    }
}