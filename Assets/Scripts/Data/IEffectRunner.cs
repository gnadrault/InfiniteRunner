using Gameplay.Elements.Effects;

namespace Data
{
    public interface IEffectRunner
    {
        void Register(WordEffect wordEffect, float duration);
    }
}