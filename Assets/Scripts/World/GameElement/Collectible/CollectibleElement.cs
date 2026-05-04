using World.Spawn;

namespace World.GameElement.Collectible
{
    public abstract class CollectibleElement: Element
    {
        public override SpawnType SpawnType => SpawnType.Collectible;
    }
}