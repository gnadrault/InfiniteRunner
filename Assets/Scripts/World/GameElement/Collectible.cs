using World.Spawn;

namespace World.GameElement
{
    public abstract class Collectible: GameElement
    {
        public override SpawnType SpawnType => SpawnType.Collectible;
    }
}