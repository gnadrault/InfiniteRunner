namespace Gameplay.Effects
{
    public interface IEffectRunner
    {
        void Register(Effect effect, float duration);

        void Stop();
    }
}