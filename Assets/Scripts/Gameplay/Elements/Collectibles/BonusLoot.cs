using Player;
using UnityEngine;

namespace Gameplay.Elements.Collectibles
{
    public class BonusLoot : Collectible
    {
        
        public override void OnPlayerCollision(PlayerController player, Transform position)
        {
            player.CollectLoot(Point);
            Destroy(gameObject);
        }
    }
}