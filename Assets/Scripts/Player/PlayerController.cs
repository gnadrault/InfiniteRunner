using UnityEngine;

namespace Player
{
    
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        [HideInInspector]
        public PlayerState currentState;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            currentState = PlayerState.Ground;
        }
    }
}
