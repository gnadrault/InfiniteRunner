using UnityEngine;

namespace Effect
{
    public class MoveHorizontal : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float leftLaneX = -4;
        [SerializeField] private float rightLaneX = 4;

        private Vector3 _direction;

        private void Start()
        {
            _direction = Random.value < 0.5f ? Vector3.left : Vector3.right;
        }

        private void Update()
        {
            Vector3 position = transform.position;
            position.x += _direction.x * speed * Time.deltaTime;
            transform.position = position;
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (transform.position.x >= rightLaneX)
            {
                _direction = Vector3.left;
            }
            else if (transform.position.x <= leftLaneX)
            {
                _direction = Vector3.right;
            }
        }
    }
}