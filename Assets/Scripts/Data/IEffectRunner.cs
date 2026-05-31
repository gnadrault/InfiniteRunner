using Effects;

namespace Data
{
    public interface IEffectRunner
    {
        void Register(WordEffect wordEffect, float duration);
    }
}