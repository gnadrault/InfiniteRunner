using UnityEngine;

namespace Effect
{
    public class MoveVertical : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float height = 5f;

        private Vector3 _direction;
        private Vector3 _initPosition;

        private void Start()
        {
            _initPosition = transform.position;
        }

        private void Update()
        {
            Vector3 position = transform.position;
            position.y += _direction.y * speed * Time.deltaTime;
            transform.position = position;
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (transform.position.y >= height)
            {
                _direction = Vector3.down;
            }
            else if (transform.position.y <= _initPosition.y)
            {
                _direction = Vector3.up;
            }
        }
    }
}