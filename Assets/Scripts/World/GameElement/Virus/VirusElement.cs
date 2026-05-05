using Player;
using World.Spawn;

namespace World.GameElement.Virus
{
    public abstract class VirusElement: Element
    {
        
        public override void OnPlayerCollision(PlayerController player)
        {
            player.AttachVirus(this);
        }
    }
}