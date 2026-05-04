using Player;
using World.Spawn;

namespace World.GameElement.Virus
{
    public class VirusElement: Element
    {
        public override SpawnType SpawnType => SpawnType.Virus;
        
        public override void OnPlayerCollision(PlayerController player)
        {
            player.AttachVirus(this);
        }
    }
}