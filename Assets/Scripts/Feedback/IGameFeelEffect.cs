using Data;

namespace Feedback
{
    public interface IGameFeelEffect
    {
        void ApplyEffect(GameFeelProfile profile);
        void ResetToAmbient();
    }
}