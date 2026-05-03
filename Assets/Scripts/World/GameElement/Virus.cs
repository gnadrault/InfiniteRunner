using Player;
using World.Spawn;

namespace World.GameElement
{
    public class Virus: GameElement
    {
        public override SpawnType SpawnType => SpawnType.Virus;
        
        public override void OnPlayerCollision(PlayerController player)
        {
            player.AttachVirus(this);
        }
    }
}