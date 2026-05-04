using UnityEngine;

namespace World.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        private GameElement.Element _element;

        private void Awake()
        {
            _element = GetComponent<GameElement.Element>();
        }

        public GameElement.Element Element => _element;
    }
}
