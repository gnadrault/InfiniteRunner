namespace World.GameElement.Virus.State
{
    public interface IVirusState
    {
        void Enter();
        void UpdateState();
        void Exit();
    }
}