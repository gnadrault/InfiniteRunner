namespace World.GameElement.Virus.State
{
    public class AttackState : IVirusState
    {
        /*IdleState
        ├── Enter()  → active le mouvement
        ├── Update() → le virus attaque le joueur
        └── Exit()   → collision -> attaché au joueur */
        
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