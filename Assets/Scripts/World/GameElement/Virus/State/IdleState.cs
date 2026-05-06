namespace World.GameElement.Virus.State
{
    public class IdleState : IVirusState
    {
        /*IdleState
        ├── Enter()  → active le mouvement
        ├── Update() → déplace le virus, idle
        └── Exit()   → a détecté le joueur -> attaque */
        
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