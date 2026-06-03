namespace Gameplay.Effects
{
    public interface IEffectRunner
    {
        void Register(WordEffect wordEffect, float duration);

        void Stop();
    }
}