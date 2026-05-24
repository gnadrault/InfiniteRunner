using Player;

namespace World.GameElement.WordEffect
{
    public class Shield: WordEffect
    {
        public override void ApplyEffect(PlayerController player)
        {
            base.ApplyEffect(player);
            player.ApplyShield();
        }

        public override void RemoveEffect(PlayerController player)
        {
            base.RemoveEffect(player);
            player.RemoveShield();
        }
    }
}