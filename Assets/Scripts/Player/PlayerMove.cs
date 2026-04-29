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
            Move(--_index);
        }

        public void MoveRight()
        {
            Move(++_index);
        }

        private void Move(int i)
        {
            _index = Mathf.Clamp(i, 0, lineAnchors.Length - 1);
        }

        public float HandlePlayerMove(float currentXPosition)
        {
            return Mathf.Lerp(currentXPosition, lineAnchors[_index].position.x, speed * Time.deltaTime);
        }
    }
}
