using Data;
using Data.Database;

namespace Feedback
{
    public interface IGameFeelEffect
    {
        void ApplyEffect(GameFeelProfile profile);
        void ResetToAmbient();
    }
}