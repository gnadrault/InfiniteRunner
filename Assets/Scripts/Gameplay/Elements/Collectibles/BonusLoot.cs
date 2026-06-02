using Player;
using UnityEngine;

namespace Gameplay.Elements.Collectibles
{
    /// <summary>
    /// Bonus loot game object
    /// </summary>
    public class BonusLoot : Collectible
    {
        
        public override void OnPlayerCollision(Transform position)
        {
            PlayerController.Instance.CollectLoot(Point);
            Destroy(gameObject);
        }
    }
}