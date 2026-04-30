namespace Player.State
{
    public interface IPlayerState
    {
        void Enter();
        void UpdateState();
        void Exit();
        bool IsDone();
    }
}