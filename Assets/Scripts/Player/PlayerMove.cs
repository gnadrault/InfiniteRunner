using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Transform[] lineAnchors;
        [SerializeField] private int initAnchorIndex;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotationSpeed = 1f;
    
        private int _index;

        private void Start()
        {
            _index = initAnchorIndex;
        }

        public void MoveLeft()
        {
            if ( _index > 0)
                _index--;
        }

        public void MoveRight()
        {
            if (_index < lineAnchors.Length - 1)
                _index++;
        }

        public float HandlePlayerMove()
        {
            return lineAnchors[_index].position.x;
        }
    }
}
