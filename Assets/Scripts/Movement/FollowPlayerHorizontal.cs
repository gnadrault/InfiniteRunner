using Core;
using Data;
using Player;
using UnityEngine;

namespace Movement
{
    public class FollowPlayerHorizontal : GameBehavior
    {
        [SerializeField] private FloatValue speed;
        [SerializeField] private float leftLaneX = -4;
        [SerializeField] private float rightLaneX = 4;
        
        private float _speed;
        
        private void Start()
        {
            _speed = speed.Value;
        }
        
        protected override void GameplayUpdate()
        {
            Vector3 position = transform.position;
            float deltaX = PlayerController.Instance.transform.position.x - position.x;
            
            if (Mathf.Abs(deltaX) < 0.05f) return; // Deadzone => prevent pixel movement
            position.x = Mathf.MoveTowards(position.x, PlayerController.Instance.transform.position.x, _speed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, leftLaneX, rightLaneX);
            transform.position = position;
        }
    }
}