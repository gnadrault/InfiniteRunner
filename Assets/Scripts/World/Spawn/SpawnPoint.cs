using UnityEngine;

namespace World.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        private GameElement.GameElement _element;

        private void Awake()
        {
            _element = GetComponent<GameElement.GameElement>();
        }

        public GameElement.GameElement Element => _element;
    }
}
