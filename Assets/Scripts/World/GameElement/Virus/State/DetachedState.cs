namespace World.GameElement.Virus.State
{
    public class DetachedState : IVirusState
    {
        /*ResolvedState
        ├── Enter()  → joue animation/effet de disparition
        ├── Update() → attend fin animation
        └── Exit()   → rien*/
        
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}