using Core;
using Data;
using UnityEngine;

namespace Movement
{
    public class FollowPlayerHorizontal : GameBehavior
    {
        [SerializeField] private FloatValue speed;
        [SerializeField] private float leftLaneX = -4;
        [SerializeField] private float rightLaneX = 4;
        
        private Transform _player;
        private float _speed;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player")?.transform;
            _speed = speed.Value;
        }
        
        protected override void GameplayUpdate()
        {
            if (!_player) return;

            Vector3 position = transform.position;
            float deltaX = _player.position.x - position.x;
            
            if (Mathf.Abs(deltaX) < 0.05f) return; // Deadzone => prevent pixel movement
            position.x = Mathf.MoveTowards(position.x, _player.position.x, _speed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, leftLaneX, rightLaneX);
            transform.position = position;
        }
    }
}